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


    [HarmonyPatch(typeof(CaravanExitMapUtility))]
    [HarmonyPatch("AddCaravanExitTaleIfShould")]
    public static class VanillaMemesExpanded_CaravanExitMapUtility_AddCaravanExitTaleIfShould_Patch
    {
        [HarmonyPostfix]
        static void SetPawnCaravanTimerToZero(Pawn pawn, List<Pawn> ___tmpPawns)
        {

            if (pawn.Spawned && pawn.IsFreeColonist)
            {
                PawnCollectionClass.AddColonistToCaravanList(pawn, 0);
                PawnCollectionClass.ResetPawnCaravanTicks(pawn);
                
            }

            foreach (Pawn p in ___tmpPawns)
            {
                PawnCollectionClass.AddColonistToCaravanList(p, 0);
                PawnCollectionClass.ResetPawnCaravanTicks(p);

            }







        }
    }








}
