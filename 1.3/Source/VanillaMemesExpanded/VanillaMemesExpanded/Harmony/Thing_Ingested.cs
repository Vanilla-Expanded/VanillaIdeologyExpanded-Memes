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


    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("Ingested")]
    public static class VanillaMemesExpanded_Thing_Ingested_Patch
    {
        [HarmonyPostfix]
        static void NotifyAnimalProducstIngested(Thing __instance, Pawn ingester)
        {
            CompIngredients compIngredients = __instance.TryGetComp<CompIngredients>();
            bool flagIngredientsAreProduct = false;
            if (compIngredients != null)
            {
                
                for (int i = 0; i < compIngredients.ingredients.Count; i++)
                {
                    if (compIngredients.ingredients[i].IsAnimalProduct)
                    {
                        flagIngredientsAreProduct = true;
                    }                   
                }
            }
            bool flagFoodItselfIsProduct = __instance.def.IsAnimalProduct;





            if (flagIngredientsAreProduct || flagFoodItselfIsProduct)
            {
               
                 Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_AteAnimalProducts, ingester.Named(HistoryEventArgsNames.Doer)), false);
              
            }







            }
    }








}
