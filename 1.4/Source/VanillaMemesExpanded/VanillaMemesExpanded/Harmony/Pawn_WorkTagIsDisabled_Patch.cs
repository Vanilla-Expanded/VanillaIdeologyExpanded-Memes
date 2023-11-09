using HarmonyLib;
using Verse;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(Pawn), "WorkTagIsDisabled")]
    public static class Pawn_WorkTagIsDisabled_Patch
    {
        public static bool Prefix(Pawn __instance, ref bool __result)
        {
            if (__instance.Ideo?.GetRole(__instance)?.def == InternalDefOf.VME_IdeoRole_Majordomo)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}