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


    [HarmonyPatch(typeof(RitualObligationTargetWorker_IdeoBuildingOrRitualSpot))]
    [HarmonyPatch("CanUseTargetInternal")]
    public static class VanillaMemesExpanded_RitualObligationTargetWorker_IdeoBuildingOrRitualSpot_CanUseTargetInternal_Patch
    {
        [HarmonyPostfix]
        static void DontAllowWickerManToActAsSpeechFocus(TargetInfo target,ref RitualTargetUseReport __result)
        {

            Building building = target.Thing as Building;
            if (building != null && building.def?.defName=="VME_WickerMan")
            {
                __result= false;
            }
           





        }
    }








}
