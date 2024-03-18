using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaMemesExpanded
{


    [HarmonyPatch(typeof(CompDrug))]
    [HarmonyPatch("PostIngested")]
    public static class VanillaMemesExpanded_CompDrug_PostIngested_Patch
    {
        [HarmonyPostfix]
        static void DetectDrinkConsumed(Pawn ingester, CompDrug __instance)
        {
            HashSet<string> allDrinks = new HashSet<string>();
            HashSet<SupportedDrinksForPreceptDefs> allLists = DefDatabase<SupportedDrinksForPreceptDefs>.AllDefsListForReading.ToHashSet();
            foreach (SupportedDrinksForPreceptDefs individualList in allLists)
            {
                allDrinks.AddRange(individualList.supportedDrinksForPrecept);
            }

            if (allDrinks.Contains(__instance.parent.def.defName))
            {
                PawnCollectionClass.AddColonistToBoozeList(ingester, 0);
                PawnCollectionClass.ResetPawnBoozeTicks(ingester);
            }


        }
    }








}
