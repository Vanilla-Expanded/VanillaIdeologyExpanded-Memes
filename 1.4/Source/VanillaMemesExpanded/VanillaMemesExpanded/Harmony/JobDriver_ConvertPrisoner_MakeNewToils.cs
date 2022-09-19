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


    [HarmonyPatch(typeof(JobDriver_ConvertPrisoner))]
    [HarmonyPatch("MakeNewToils")]
    public static class VanillaMemesExpanded_JobDriver_ConvertPrisoner_MakeNewToils_Patch
    {
        [HarmonyPostfix]
        static void ReduceAnonymity(JobDriver_ConvertPrisoner __instance)
        {

            if (__instance.pawn.Ideo?.HasPrecept(InternalDefOf.VME_Anonymity_Required) == true)
            {
                if (__instance.pawn.needs != null)
                {
                    Need_Anonymity need = __instance.pawn.needs.TryGetNeed<Need_Anonymity>();
                    need.AnonymityTaken(-0.5f);
                    need.CurLevel -= 0.5f;

                }
            }







        }
    }








}
