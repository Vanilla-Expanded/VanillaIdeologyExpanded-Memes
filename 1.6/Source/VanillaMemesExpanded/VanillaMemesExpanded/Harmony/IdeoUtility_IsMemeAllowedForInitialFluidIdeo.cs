using HarmonyLib;
using RimWorld;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection.Emit;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(IdeoUtility))]
    [HarmonyPatch("IsMemeAllowedForInitialFluidIdeo")]
    public static class VanillaMemesExpanded_IdeoUtility_IsMemeAllowedForInitialFluidIdeo_Patch
    {
        
        [HarmonyPostfix]
        static void AllowAllMemes(ref bool __result)
        {
            if (VanillaMemesExpanded_Settings.allowHighImpactMemesForFluidIdeos)
            {
                __result = true;
            }
        }
    }
}