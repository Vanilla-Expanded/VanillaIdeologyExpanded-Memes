using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(Pawn_RoyaltyTracker))]
    [HarmonyPatch("TryRemoveFavor")]
    public static class VanillaMemesExpanded_Pawn_RoyaltyTracker_TryRemoveFavor_Patch
    {
        [HarmonyPrefix]
        static void AdjustFavorCost(Pawn_RoyaltyTracker __instance, Faction faction, ref int amount)
        {
            if (__instance.pawn.Ideo?.HasPrecept(InternalDefOf.VME_PermitHonorCost_Doubled) == true)
            {
                amount *= 2;
            }
            else if (__instance.pawn.Ideo?.HasPrecept(InternalDefOf.VME_PermitHonorCost_Halved) == true)
            {
                amount /= 2;
            }
        }
    }
}
