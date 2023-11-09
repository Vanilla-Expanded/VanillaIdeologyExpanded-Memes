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


    [HarmonyPatch(typeof(StatWorker))]
    [HarmonyPatch("IsDisabledFor")]
    public static class VanillaMemesExpanded_StatWorker_IsDisabledFor_Patch
    {
        [HarmonyPostfix]
        static void AlwaysEnabled(Thing thing, ref bool __result)
        {
            Pawn pawn = thing as Pawn;

            if(pawn!=null&&((Precept_RoleSingle)pawn.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_Majordomo))?.ChosenPawnSingle() == pawn)
            {
                __result = false;
            }



        }
    }








}
