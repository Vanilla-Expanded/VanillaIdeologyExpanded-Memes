using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(Pawn), "WorkTypeIsDisabled")]
    public static class Pawn_WorkTypeIsDisabled_Patch
    {
        public static bool Prefix(Pawn __instance, ref bool __result, WorkTypeDef w)
        {
            if (w.workTags.HasFlag(WorkTags.Violent) is false && ((Precept_RoleSingle)__instance.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_Majordomo))?.ChosenPawnSingle() == __instance)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}