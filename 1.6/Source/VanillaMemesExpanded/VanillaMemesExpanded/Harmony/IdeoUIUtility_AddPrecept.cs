using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection.Emit;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(IdeoUIUtility))]
    [HarmonyPatch("AddPrecept")]
    public static class VanillaMemesExpanded_IdeoUIUtility_AddPrecept_Patch
    {
        
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();

            for (var i = 0; i < codes.Count; i++)
            {
                var code = codes[i];
                if (i > 0 && codes[i - 1].opcode == OpCodes.Ldloc_2 && codes[i].opcode == OpCodes.Ldc_I4_6)
                {
                    yield return new CodeInstruction(OpCodes.Ldc_I4, (int)VanillaMemesExpanded_Settings.ritualsAmount);
                }
                else if (i > 0 && codes[i - 1].opcode == OpCodes.Ldstr && (string)codes[i - 1].operand == "MaxRitualCount")
                {
                    yield return new CodeInstruction(OpCodes.Ldc_I4, (int)VanillaMemesExpanded_Settings.ritualsAmount);
                }
                else
                {
                    yield return code;
                }
            }
        }
    }
}