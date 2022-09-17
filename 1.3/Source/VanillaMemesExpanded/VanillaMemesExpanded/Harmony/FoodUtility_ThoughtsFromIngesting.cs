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


    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("ThoughtsFromIngesting")]
    public static class VanillaMemesExpanded_FoodUtility_ThoughtsFromIngesting_Patch
    {
        [HarmonyPostfix]
        static void NotifyAnimalProducstIngested(Pawn ingester, Thing foodSource, ThingDef foodDef)
        {
            CompIngredients compIngredients = foodSource.TryGetComp<CompIngredients>();
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
            bool flagFoodItselfIsProduct = foodSource.def.IsAnimalProduct;

           

            if (flagIngredientsAreProduct || flagFoodItselfIsProduct)
            {
                MethodInfo methodInfo = typeof(FoodUtility).GetMethod("AddThoughtsFromIdeo", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                var parameters = new object[] { InternalDefOf.VME_AteAnimalProducts, ingester, foodDef, MeatSourceCategory.Undefined };
                methodInfo.Invoke(null, parameters);

              
            }







            }
    }








}
