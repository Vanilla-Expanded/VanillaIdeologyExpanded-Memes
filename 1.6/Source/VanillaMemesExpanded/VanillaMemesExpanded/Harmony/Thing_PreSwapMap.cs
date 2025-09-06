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

    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("PreSwapMap")]
    public static class VanillaMemesExpanded_Thing_PreSwapMap_Patch
    {
        [HarmonyPostfix]
        static void SetPawnCaravanTimerToZero(Thing __instance)
        {
            if (__instance is Pawn pawn && pawn.Spawned && pawn.IsFreeColonist)
            {
                WorldComponent_TravellingAndTradingTracker.Instance.AddColonistToCaravanList(pawn, 0);
                WorldComponent_TravellingAndTradingTracker.Instance.ResetPawnCaravanTicks(pawn);
            }

        }
    }
}
