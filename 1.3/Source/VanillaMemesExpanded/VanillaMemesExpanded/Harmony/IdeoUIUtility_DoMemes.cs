using HarmonyLib;
using RimWorld;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection.Emit;



namespace VanillaMemesExpanded
{


    [HarmonyPatch(typeof(IdeoUIUtility))]
    [HarmonyPatch("DoMemes")]
    public static class VanillaMemesExpanded_IdeoUIUtility_DoMemes_Patch
    {
        [HarmonyPrefix]
        static void MakeBoxSmaller(ref Vector2 ___MemeBoxSize,Ideo ideo)
        {
            if (ideo.memes.Count > 6) { ___MemeBoxSize =  new Vector2(80f, 120f); }
            
        }

        [HarmonyPostfix]
        static void MakeBoxBigger(ref Vector2 ___MemeBoxSize, Ideo ideo)
        {
            if (ideo.memes.Count > 6)
            {
                ___MemeBoxSize = new Vector2(122f, 120f);
            }
        }
    }
}