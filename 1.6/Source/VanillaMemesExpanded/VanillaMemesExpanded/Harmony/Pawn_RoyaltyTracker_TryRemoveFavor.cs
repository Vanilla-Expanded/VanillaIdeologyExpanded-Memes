using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Reflection.Emit;
using System;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(Pawn_RoyaltyTracker))]
    [HarmonyPatch("TryRemoveFavor")]
    public static class VanillaMemesExpanded_Pawn_RoyaltyTracker_TryRemoveFavor_Patch
    {
       

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
          
            var adjustFavorCost = AccessTools.Method(typeof(VanillaMemesExpanded_Pawn_RoyaltyTracker_TryRemoveFavor_Patch), "AdjustFavor");
            foreach (CodeInstruction instruction in codeInstructions)
            {
               
                if (instruction.opcode==OpCodes.Ldarg_2)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Call, adjustFavorCost);
                }
                else { yield return instruction; }
            }
        }

        public static int AdjustFavor(Pawn_RoyaltyTracker tracker, int amount)
        {
            if (tracker.pawn.Ideo?.HasPrecept(InternalDefOf.VME_PermitHonorCost_Doubled) == true)
            {
                return amount * 2;
            }

            if (tracker.pawn.Ideo?.HasPrecept(InternalDefOf.VME_PermitHonorCost_Halved) == true)
            {
                return (int)(amount / 2);
            }
            return amount;
        }
    }
}
