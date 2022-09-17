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


    [HarmonyPatch(typeof(JobDriver))]
    [HarmonyPatch("Notify_Starting")]
    public static class VanillaMemesExpanded_JobDriver_Notify_Starting_Patch
    {
        [HarmonyPostfix]
        static void IfDumbSendHistoryEvent(JobDriver __instance)
        {


            if (__instance.job?.workGiverDef?.workType?.workTags.HasFlag(WorkTags.ManualDumb) == true)
            {

                Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_DumbLabor, new SignalArgs(__instance.pawn.Named(HistoryEventArgsNames.Doer))), true);

               


            }

            if (__instance.job?.workGiverDef?.workType?.workTags.HasFlag(WorkTags.Firefighting) == true)
            {
                if ((__instance.pawn?.Ideo?.HasMeme(InternalDefOf.VME_FireWorship) == true)&& ((Precept_RoleSingle)__instance.pawn?.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_FireKeeper)).ChosenPawnSingle()!= __instance.pawn) {
                    
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_Firefighting, new SignalArgs(__instance.pawn.Named(HistoryEventArgsNames.Doer))), true);

                }
               



            }







        }
    }








}
