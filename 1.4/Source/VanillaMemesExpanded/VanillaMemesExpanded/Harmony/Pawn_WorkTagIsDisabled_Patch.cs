using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(Pawn), "WorkTagIsDisabled")]
    public static class Pawn_WorkTagIsDisabled_Patch
    {
        public static bool Prefix(Pawn __instance, ref bool __result)
        {
            if (((Precept_RoleSingle)__instance.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_Majordomo)).ChosenPawnSingle() == __instance)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}