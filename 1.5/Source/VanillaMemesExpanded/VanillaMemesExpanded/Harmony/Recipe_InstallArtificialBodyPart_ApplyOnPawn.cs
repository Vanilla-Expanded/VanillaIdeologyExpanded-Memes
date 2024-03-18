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


               if( __instance.recipe?.addsHediff?.spawnThingOnRemoved?.thingCategories?.Contains(ThingCategoryDef.Named("BodyPartsNatural")) == false &&

                        __instance.recipe?.addsHediff?.spawnThingOnRemoved?.thingCategories?.Contains(DefDatabase<ThingCategoryDef>.GetNamedSilentFail("AA_ImplantCategory")) == false &&
                        __instance.recipe?.addsHediff?.spawnThingOnRemoved?.thingCategories?.Contains(DefDatabase<ThingCategoryDef>.GetNamedSilentFail("VFEI_BodyPartsInsect")) == false &&
                        __instance.recipe?.addsHediff?.spawnThingOnRemoved?.thingCategories?.Contains(DefDatabase<ThingCategoryDef>.GetNamedSilentFail("GR_ImplantCategory")) == false)
                {

                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_InstalledNonNaturalProsthetic, billDoer.Named(HistoryEventArgsNames.Doer)), true);
                }


            }


            











        }
    }








}
