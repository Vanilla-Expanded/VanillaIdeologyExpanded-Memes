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


    [HarmonyPatch(typeof(WorkGiver_Warden_Convert))]
    [HarmonyPatch("JobOnThing")]
    public static class VanillaMemesExpanded_WorkGiver_Warden_Convert_JobOnThing_Patch
    {
        /*[HarmonyPrefix]
        static bool DisableWardenConversion(Job __result, Pawn pawn)
        {

            
            if (pawn.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Proselytizing_Never)==true)
            {
                __result = null;
            }

            
            return true;




        }*/
    }








}
