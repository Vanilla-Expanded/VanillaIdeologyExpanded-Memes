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
   

    [HarmonyPatch(typeof(Plant))]
    [HarmonyPatch("YieldNow")]
    public static class VanillaMemesExpanded_Plant_YieldNow_Patch
    {
        [HarmonyPostfix]
        static void IncreaseTreeCuttingYield(Plant __instance,ref int __result)
        {
            if ((__instance?.def?.plant?.IsTree==true && Current.Game.World.factionManager.OfPlayer.ideos?.GetPrecept(InternalDefOf.VME_WoodcuttingYield_High)!=null)||
                (__instance?.IsCrop ==true && Current.Game.World.factionManager.OfPlayer.ideos?.GetPrecept(InternalDefOf.VME_FarmingYield_High) != null))
            {
                __result = GenMath.RoundRandom(__result* 1.1f);

            }

            if (Current.Game.World.factionManager.OfPlayer.ideos?.GetPrecept(InternalDefOf.VME_PermanentBases_Despised) != null) 
            {
                if(__instance?.def?.plant?.IsTree == false && __instance.IsCrop)
                {
                    __result = GenMath.RoundRandom(__result * 0.9f);
                }
                if (__instance?.def?.plant?.IsTree == false && !__instance.IsCrop)
                {
                    __result = GenMath.RoundRandom(__result * 1.4f);
                }


            }



        }
    }








}
