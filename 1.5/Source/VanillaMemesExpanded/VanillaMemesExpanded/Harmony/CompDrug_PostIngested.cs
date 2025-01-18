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
            if (StaticCollections.drinksForAlcoholPrecepts.Contains(__instance.parent.def.defName) || __instance.Props.chemical == ChemicalDefOf.Alcohol)
            {
                WorldComponent_AlcoholScarsAndSlaveryTracker.Instance.AddColonistToBoozeList(ingester, 0);
                WorldComponent_AlcoholScarsAndSlaveryTracker.Instance.ResetPawnBoozeTicks(ingester);
            }
        }
    }
}
