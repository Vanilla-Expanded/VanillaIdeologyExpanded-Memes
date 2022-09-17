using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System;
using Verse.AI;



namespace VanillaMemesExpanded
{


    [HarmonyPatch(typeof(ExpectationsUtility))]
    [HarmonyPatch("CurrentExpectationFor")]
    [HarmonyPatch(new Type[] { typeof(Pawn) })]
    public static class VanillaMemesExpanded_ExpectationsUtility_CurrentExpectationFor_Patch
    {
        [HarmonyPostfix]
        static void LowerOrDecreaseExpectation(Pawn p, ref ExpectationDef __result)
        {


            if (p?.Ideo?.HasPrecept(InternalDefOf.VME_Expectations_High) == true)
            {
                ExpectationDef finalResult = __result;

                

                if (__result == ExpectationDefOf.ExtremelyLow)
                {
                   finalResult = ExpectationDefOf.VeryLow;                     
                }else if (__result == ExpectationDefOf.VeryLow)
                {
                    finalResult = ExpectationDefOf.Low;
                }
                else if (__result == ExpectationDefOf.Low)
                {
                    finalResult = ExpectationDefOf.Moderate;
                }
                else if (__result == ExpectationDefOf.Moderate)
                {
                    finalResult = ExpectationDefOf.High;
                }
                else if (__result == ExpectationDefOf.High)
                {
                    finalResult = ExpectationDefOf.SkyHigh;
                }
                else if (__result == ExpectationDefOf.SkyHigh)
                {
                    finalResult = ExpectationDefOf.SkyHigh;
                }
                __result = finalResult;

            }
            if (p?.Ideo?.HasPrecept(InternalDefOf.VME_Expectations_Low) == true)
            {
                ExpectationDef finalResult = __result;
                if (__result == ExpectationDefOf.ExtremelyLow)
                {
                    finalResult = ExpectationDefOf.ExtremelyLow;
                }
                else if (__result == ExpectationDefOf.VeryLow)
                {
                    finalResult = ExpectationDefOf.ExtremelyLow;
                }
                else if (__result == ExpectationDefOf.Low)
                {
                    finalResult = ExpectationDefOf.VeryLow;
                }
                else if (__result == ExpectationDefOf.Moderate)
                {
                    finalResult = ExpectationDefOf.Low;
                }
                else if (__result == ExpectationDefOf.High)
                {
                    finalResult = ExpectationDefOf.Moderate;
                }
                else if (__result == ExpectationDefOf.SkyHigh)
                {
                    finalResult = ExpectationDefOf.High;
                }
                __result = finalResult;

                


            }







        }
    }








}
