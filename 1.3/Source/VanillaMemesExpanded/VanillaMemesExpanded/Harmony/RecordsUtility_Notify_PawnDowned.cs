using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaMemesExpanded
{


    [HarmonyPatch(typeof(RecordsUtility))]
    [HarmonyPatch("Notify_PawnDowned")]
    public static class VanillaMemesExpanded_RecordsUtility_Notify_PawnDowned_Patch
    {
        [HarmonyPostfix]
        static void ThrowPawnDownedEvent(Pawn downed, Pawn instigator)
        {
            if (instigator!=null&&!instigator.NonHumanlikeOrWildMan()&& instigator.Faction?.IsPlayer != true) {

                if (downed.needs?.mood?.thoughts?.memories?.GetFirstMemoryOfDef(InternalDefOf.VME_Defeat_Dishonorable) != null)
                {
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_SecondDefeat, downed.Named(HistoryEventArgsNames.Doer)), true);
                } else Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_Defeat, downed.Named(HistoryEventArgsNames.Doer)), true);




            }



        }
    }








}
