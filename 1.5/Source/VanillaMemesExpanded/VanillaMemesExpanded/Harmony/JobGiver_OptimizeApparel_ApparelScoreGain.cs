
using HarmonyLib;
using RimWorld;
using Verse;
using System;
using System.Collections.Generic;



namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(JobGiver_OptimizeApparel), "ApparelScoreGain")]
    internal class VanillaMemesExpanded_JobGiver_OptimizeApparel_ApparelScoreGain_Postfix
    {


        [HarmonyPostfix]
        private static void PostFix(ref float __result, Pawn pawn, Apparel ap)
        {
            if (pawn.Ideo?.HasPrecept(InternalDefOf.VME_LeatherApparel_Abhorrent)==true 
                && ap.Stuff?.stuffProps?.categories?.Contains(StuffCategoryDefOf.Leathery) == true)
            {
                __result = -1000;

            }


        }
    }
}