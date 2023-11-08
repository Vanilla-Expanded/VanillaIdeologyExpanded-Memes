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


    [HarmonyPatch(typeof(Pawn_RoyaltyTracker))]
    [HarmonyPatch("SetFavor")]
    public static class VanillaMemesExpanded_Pawn_RoyaltyTracker_SetFavor_Patch
    {
        [HarmonyPostfix]
        static void AddExtraFavor(Dictionary<Faction, int> ___favor,Pawn_RoyaltyTracker __instance, Faction faction, int amount)
        {
            if(amount<0) {
                if (__instance.pawn.Ideo?.HasPrecept(InternalDefOf.VME_PermitHonorCost_Doubled) == true)
                {
                    ___favor.SetOrAdd(faction, amount);
                }

                if (__instance.pawn.Ideo?.HasPrecept(InternalDefOf.VME_PermitHonorCost_Halved) == true)
                {
                    ___favor.SetOrAdd(faction, -amount/2);
                }

            }
            






        }
    }








}
