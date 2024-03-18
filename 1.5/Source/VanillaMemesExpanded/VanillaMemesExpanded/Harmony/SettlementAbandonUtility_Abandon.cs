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


    [HarmonyPatch(typeof(SettlementAbandonUtility))]
    [HarmonyPatch("Abandon")]
    public static class VanillaMemesExpanded_SettlementAbandonUtility_Abandon_Patch
    {
        [HarmonyPostfix]
        static void SetAbandonedTimeToZero()
        {
            
                    PawnCollectionClass.ticksWithoutAbandoning = 0;
                



        }
    }








}
