using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(QualityUtility), nameof(QualityUtility.GenerateQualityCreatedByPawn), [typeof(Pawn), typeof(SkillDef), typeof(bool)])]
    public static class VanillaMemesExpanded_QualityUtility_GenerateQualityCreatedByPawn_Patch
    {
        public static void Postfix(Pawn pawn, SkillDef relevantSkill, ref QualityCategory __result)
        {
            if (relevantSkill != SkillDefOf.Construction)
            {
                var ideo = pawn?.Ideo;

                if (ideo?.HasPrecept(InternalDefOf.VME_CraftingQuality_Increased) == true)
                {
                    __result = (QualityCategory)Math.Min((int)__result + 1, (int)QualityCategory.Legendary);
                }
                if (ideo?.HasPrecept(InternalDefOf.VME_CraftingQuality_Decreased) == true)
                {
                    __result = (QualityCategory)Math.Max((int)__result - 1, (int)QualityCategory.Awful);
                }
            }
        }
    }
}
