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

    [HarmonyPatch(typeof(Recipe_InstallArtificialBodyPart))]
    [HarmonyPatch("ApplyOnPawn")]
    public static class VanillaMemesExpanded_Recipe_InstallArtificialBodyPart_ApplyOnPawn_Patch
    {
        [HarmonyPostfix]
        static void InstalledNonNaturalBodyPart(Pawn pawn, Recipe_InstallArtificialBodyPart __instance, Pawn billDoer)
        {
            if (billDoer != null)
            {
                ThingDef implant = __instance.recipe?.addsHediff?.spawnThingOnRemoved;
                if (implant!=null && !StaticCollections.naturalImplants.Contains(implant.defName))
                {
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_InstalledNonNaturalProsthetic, billDoer.Named(HistoryEventArgsNames.Doer)), true);
                }             
            }
        }
    }
}
