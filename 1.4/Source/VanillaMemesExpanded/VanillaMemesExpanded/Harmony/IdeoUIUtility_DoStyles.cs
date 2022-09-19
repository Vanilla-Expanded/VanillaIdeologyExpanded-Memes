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
    [HarmonyPatch("DoStyles")]
    public static class VanillaMemesExpanded_IdeoUIUtility_DoStyles_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {


            var codes = instructions.ToList();

            for (var i = 0; i < codes.Count; i++)
            {

                var code = codes[i];
                if (i > 0 && codes[i - 1].opcode == OpCodes.Ldloc_1 && codes[i].opcode == OpCodes.Ldc_I4_3)
                {

                    yield return new CodeInstruction(OpCodes.Ldc_I4, (int)VanillaMemesExpanded_Settings.stylesAmount);
                }
                
                else
                {
                    yield return code;
                }


            }


        }
    }
}