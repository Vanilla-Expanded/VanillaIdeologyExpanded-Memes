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
                StaticCollections.AddColonistToCaravanList(pawn, 0);
                StaticCollections.ResetPawnCaravanTicks(pawn);
                
            }

            foreach (Pawn p in ___tmpPawns)
            {
                StaticCollections.AddColonistToCaravanList(p, 0);
                StaticCollections.ResetPawnCaravanTicks(p);

            }







        }
    }








}
