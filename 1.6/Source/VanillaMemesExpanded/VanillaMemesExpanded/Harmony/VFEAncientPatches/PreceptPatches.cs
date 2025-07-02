using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace VanillaMemesExpanded;

public static class PreceptPatches
{
    public static Dictionary<PrisonerInteractionModeDef, HistoryEventDef> PrisonerHistory;

    static PreceptPatches()
    {
        LongEventHandler.ExecuteWhenFinished(() =>
        {
            PrisonerHistory = new();
            foreach (var def in DefDatabase<HistoryEventDef>.AllDefs)
                if (def.TryGetModExtension<RelatedInteractionMode>(out var ext) && ext.related != null)
                    PrisonerHistory[ext.related] = def;
        });
    }

    public static void Do(Harmony harm)
    {
        harm.Patch(AccessTools.Method(typeof(Ideo), nameof(Ideo.MemberWillingToDo)),
            new(typeof(PreceptPatches), nameof(MemberWillingToDo_Prefix)));
        harm.Patch(AccessTools.Method(typeof(WorkGiver_Warden), "ShouldTakeCareOfPrisoner"),
            postfix: new(typeof(PreceptPatches), nameof(ShouldTakeCareOfPrisoner_Postfix)));
        harm.Patch(typeof(ITab_Pawn_Visitor).GetNestedTypes(AccessTools.all)
               .SelectMany(AccessTools.GetDeclaredMethods)
               .First(m => m.Name.Contains("DoPrisonerTab") && m.Name.Contains("CanUsePrisonerInteractionMode")),
            postfix: new(typeof(PreceptPatches), nameof(CanUseInteractionMode_Postfix)));
        harm.Patch(AccessTools.Method(typeof(ITab_Pawn_Visitor), "InteractionModeChanged"),
            postfix: new(typeof(PreceptPatches), nameof(InteractionModeChanged_Postfix)));
        harm.Patch(AccessTools.Method(typeof(JobGiver_ReactToCloseMeleeThreat), "TryGiveJob"),
            postfix: new(typeof(PreceptPatches), nameof(NoFightingInterrogator)));
    }

    public static void NoFightingInterrogator(Pawn pawn, ref Job __result)
    {
        var threat = pawn?.mindState?.meleeThreat;
        if (__result != null && threat != null && pawn.IsPrisoner && pawn.HostFaction == threat.Faction &&
            pawn.guest.ExclusiveInteractionMode == InternalDefOf.VFEA_Interrogate &&
            threat.CurJobDef == InternalDefOf.VFEA_PrisonerInterrogate && RestraintsUtility.InRestraints(pawn) &&
            pawn.GetRoom() is { IsPrisonCell: true } room)
            __result = JobMaker.MakeJob(JobDefOf.FleeAndCower, room.Cells?.RandomElement() ?? pawn.Position, threat);
    }

    public static void ShouldTakeCareOfPrisoner_Postfix(Pawn warden, Thing prisoner, ref bool __result)
    {
        if (__result)    {
            Pawn pawn = prisoner as Pawn;
            if (pawn != null && pawn.guest != null && pawn.guest.ExclusiveInteractionMode != null && warden.Ideo != null) {
                if (PrisonerHistory.ContainsKey(pawn.guest.ExclusiveInteractionMode))
                {
                    var eventDef = PrisonerHistory[pawn.guest.ExclusiveInteractionMode];
                    var ev = new HistoryEvent(eventDef, warden.Named(HistoryEventArgsNames.Doer), pawn.Named(HistoryEventArgsNames.Victim),
                        pawn.Faction.Named(HistoryEventArgsNames.AffectedFaction));
                    if (!ev.DoerWillingToDo()) __result = false;
                    if (!ev.VictimWillingToDo()) __result = false;
                }

                if (pawn.guest.ExclusiveInteractionMode.GetModExtension<RequirePrecept>() is { precept: var precept })
                    if (!warden.Ideo.HasPrecept(precept))
                        __result = false;

            }
            
         


            
        }
    }

    public static bool MemberWillingToDo_Prefix(HistoryEvent ev, Ideo __instance, ref bool __result)
    {
        if (ev.def.TryGetModExtension<RequirePrecept>(out var ext) && ext.precept != null && !__instance.HasPrecept(ext.precept))
        {
            __result = false;
            return false;
        }

        return true;
    }

    public static void CanUseInteractionMode_Postfix(Pawn pawn, PrisonerInteractionModeDef mode, ref bool __result)
    {
        if (!__result) return;
        if (PrisonerHistory.TryGetValue(mode, out var eventDef) &&
            !new HistoryEvent(eventDef, pawn.Named(HistoryEventArgsNames.Victim)).VictimWillingToDo()) __result = false;
        if (pawn.MapHeld is { } map && PrisonerHistory.TryGetValue(mode)?.GetModExtension<RequirePrecept>()?.precept is { } precept &&
            !map.mapPawns.FreeColonistsSpawned.Any(p => p.workSettings.WorkIsActive(WorkTypeDefOf.Warden) && p.Ideo != null && p.Ideo.HasPrecept(precept)))
            __result = false;
    }

    public static void InteractionModeChanged_Postfix(PrisonerInteractionModeDef newMode, ITab_Pawn_Visitor __instance)
    {
        if (newMode == PrisonerInteractionModeDefOf.AttemptRecruit && ReflectionCache.selPawn(__instance).MapHeld is { } map &&
            !map.mapPawns.FreeColonistsSpawned.Any(pawn =>
                pawn.workSettings.WorkIsActive(WorkTypeDefOf.Warden) &&
                new HistoryEvent(PrisonerHistory[PrisonerInteractionModeDefOf.AttemptRecruit], pawn.Named(HistoryEventArgsNames.Doer)).DoerWillingToDo()))
            Messages.Message("VFEAncients.MessageNoWardenCapableOfRecruitment".Translate(), new(ReflectionCache.selPawn(__instance)), MessageTypeDefOf.CautionInput,
                false);
    }

    public static bool VictimWillingToDo(this HistoryEvent ev)
    {
        var pawn = ev.args.GetArg<Pawn>(HistoryEventArgsNames.Victim);
        return pawn?.Ideo == null || pawn.Ideo.PreceptsListForReading.SelectMany(precept => precept.def.comps)
           .OfType<IVictimPreceptComp>()
           .All(pc => pc.VictimWillingToDo(ev));
    }
}
