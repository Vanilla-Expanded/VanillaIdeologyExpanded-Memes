using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using System;



namespace VanillaMemesExpanded
{


    [HarmonyPatch(typeof(GenLeaving))]
    [HarmonyPatch("GetBuildingResourcesLeaveCalculator")]
    public static class VanillaMemesExpanded_GenLeaving_GetBuildingResourcesLeaveCalculator_Patch
    {
        [HarmonyPostfix]
        static void SetYieldTo100(ref Func<int, int> __result, Thing destroyedThing, DestroyMode mode)
        {
            if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_JunkDeconstructionYield_High) != null)
            {
                switch (mode)
                {
                   
                    case DestroyMode.KillFinalize:
                        __result = (int count) => GenMath.RoundRandom((float)count * 1f);
                        break;                      
                    case DestroyMode.Deconstruct:
                        __result = (int count) => GenMath.RoundRandom((float)count * 1f);
                        break;
                    case DestroyMode.FailConstruction:
                        __result = (int count) => GenMath.RoundRandom((float)count * 1f);
                        break;


                }
            }

        }
    }








}
