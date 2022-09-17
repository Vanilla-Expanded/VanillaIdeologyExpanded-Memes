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


    [HarmonyPatch(typeof(GenRecipe))]
    [HarmonyPatch("PostProcessProduct")]
    public static class VanillaMemesExpanded_GenRecipe_PostProcessProduct_Patch
    {
        [HarmonyPostfix]
        static void IncreaseQualityByOne(Thing product, RecipeDef recipeDef, Pawn worker)
        {

           
                if (worker?.Ideo?.HasPrecept(InternalDefOf.VME_CraftingQuality_Increased) == true)
                {

                    CompQuality compQuality = product?.TryGetComp<CompQuality>();
                    if (compQuality != null)
                    {
                        if (recipeDef?.workSkill == null)
                        {
                            Log.Error(recipeDef + " needs workSkill because it creates a product with a quality.");
                        }
                        if (compQuality.Quality != QualityCategory.Legendary)
                        {
                            compQuality.SetQuality(compQuality.Quality + 1, ArtGenerationContext.Colony);

                        }

                    }

                }
                if (worker?.Ideo?.HasPrecept(InternalDefOf.VME_CraftingQuality_Decreased) == true)
                {

                    CompQuality compQuality = product?.TryGetComp<CompQuality>();
                    if (compQuality != null)
                    {
                        if (recipeDef?.workSkill == null)
                        {
                            Log.Error(recipeDef + " needs workSkill because it creates a product with a quality.");
                        }
                        if (compQuality.Quality != QualityCategory.Awful)
                        {
                            compQuality.SetQuality(compQuality.Quality - 1, ArtGenerationContext.Colony);

                        }

                    }

                }

                if (worker?.Ideo?.HasPrecept(DefDatabase<PreceptDef>.GetNamedSilentFail("VME_BookWriting_Exalted")) == true)
                {
                    if (product?.HasThingCategory(DefDatabase<ThingCategoryDef>.GetNamedSilentFail("VBE_Books")) == true && worker != null)
                    {
                        Find.HistoryEventsManager.RecordEvent(new HistoryEvent(DefDatabase<HistoryEventDef>.GetNamedSilentFail("VME_WroteBook"), worker.Named(HistoryEventArgsNames.Doer)), true);

                    }
                }

            
            





            }
        }








}
