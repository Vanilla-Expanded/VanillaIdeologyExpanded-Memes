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


    [HarmonyPatch(typeof(JoyUtility))]
    [HarmonyPatch("JoyTickCheckEnd")]
    public static class VanillaMemesExpanded_JoyUtility_JoyTickCheckEnd_Patch
    {
        [HarmonyPostfix]
        static void InformJoyTakingPlace(Pawn pawn)
        {
            if (pawn.IsHashIntervalTick(2000)) {
                Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_HavingFun, pawn.Named(HistoryEventArgsNames.Doer)), true);
            }





        }
    }








}
