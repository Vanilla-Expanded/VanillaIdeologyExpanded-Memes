using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(GenRecipe))]
    [HarmonyPatch("PostProcessProduct")]
    public static class VanillaMemesExpanded_GenRecipe_PostProcessProduct_Patch
    {
        public static void Postfix(Thing product, RecipeDef recipeDef, Pawn worker)
        {
            if (worker?.Ideo?.HasPrecept(InternalDefOf.VME_BookWriting_Exalted) == true)
            {
                if (product?.HasThingCategory(InternalDefOf.Books) == true && worker != null)
                {
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_WroteBook, worker.Named(HistoryEventArgsNames.Doer)), true);
                }
            }
        }
    }
}
