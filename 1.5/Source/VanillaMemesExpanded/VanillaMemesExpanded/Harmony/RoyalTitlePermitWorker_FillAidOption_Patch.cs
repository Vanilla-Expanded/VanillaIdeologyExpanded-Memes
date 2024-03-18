using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(RoyalTitlePermitWorker), "FillAidOption")]
    public static class RoyalTitlePermitWorker_FillAidOption_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var favorCostField = AccessTools.Field(typeof(RoyalAid), nameof(RoyalAid.favorCost));
            var interceptFavorCostInfo = AccessTools.Method(typeof(RoyalTitlePermitWorker_FillAidOption_Patch), "InterceptFavorCost");
            foreach (CodeInstruction instruction in codeInstructions)
            {
                yield return instruction;
                if (instruction.LoadsField(favorCostField))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call, interceptFavorCostInfo);
                }
            }
        }

        public static int InterceptFavorCost(int favorCost, Pawn pawn)
        {
            if (pawn.Ideo != null)
            {
                if (pawn.Ideo.HasPrecept(InternalDefOf.VME_PermitHonorCost_Doubled))
                {
                    return favorCost * 2;
                }
                else if (pawn.Ideo.HasPrecept(InternalDefOf.VME_PermitHonorCost_Halved))
                {
                    return favorCost / 2;
                }
            }
            return favorCost;
        }
    }
}