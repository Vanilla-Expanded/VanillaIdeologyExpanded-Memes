using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(Pawn_RoyaltyTracker))]
    [HarmonyPatch("TryRemoveFavor")]
    public static class VanillaMemesExpanded_Pawn_RoyaltyTracker_TryRemoveFavor_Patch
    {
        [HarmonyPostfix]
        static void AddExtraFavor(Dictionary<Faction, int> ___favor, Pawn_RoyaltyTracker __instance, Faction faction, int amount)
        {
            int num = __instance.GetFavor(faction);


            if (__instance.pawn.Ideo?.HasPrecept(InternalDefOf.VME_PermitHonorCost_Doubled) == true)
            {
                __instance.SetFavor(faction, num - amount);
            }

            if (__instance.pawn.Ideo?.HasPrecept(InternalDefOf.VME_PermitHonorCost_Halved) == true)
            {
                if(__instance.GetFavor(faction)> num)
                {
                    __instance.SetFavor(faction, num + (amount / 2));
                }                
            }
        }
    }
}
