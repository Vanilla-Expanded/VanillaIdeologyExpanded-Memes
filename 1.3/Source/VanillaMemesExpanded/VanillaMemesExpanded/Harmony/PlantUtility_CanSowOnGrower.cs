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


    [HarmonyPatch(typeof(Command_SetPlantToGrow))]
    [HarmonyPatch("IsPlantAvailable")]
    public static class VanillaMemesExpanded_Command_SetPlantToGrow_IsPlantAvailable_Patch
    {
        [HarmonyPostfix]
        public static void MakeCocoaBushNotSowable(ref bool __result, ThingDef plantDef)
        {
           if (plantDef.plant.sowTags.Contains("VCE_ChocolateBushSowableDetection"))
            {
                if (Current.Game.World.factionManager.OfPlayer.ideos.HasAnyIdeoWithMeme(DefDatabase<MemeDef>.GetNamedSilentFail("VME_SweetTeeth"))) {

                    __result = __result&&true;
                } else __result = false;
            }







        }
    }








}
