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


    [HarmonyPatch(typeof(MentalBreakWorker))]
    [HarmonyPatch("BreakCanOccur")]
    public static class VanillaMemesExpanded_MentalBreakWorker_BreakCanOccur_Patch
    {
        [HarmonyPostfix]
        static void DisableMostMentalBreaksIfPacifist(ref bool __result, Pawn pawn, MentalBreakWorker __instance)
        {

            if (pawn.Ideo?.HasPrecept(InternalDefOf.VME_Violence_Abhorrent)==true)
            {

                if (__instance.def.defName!= "Wander_Psychotic")
                {

                    __result = false;
                }

            }





        }
    }








}
