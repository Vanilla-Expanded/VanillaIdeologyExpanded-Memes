using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaMemesExpanded
{


    [HarmonyPatch(typeof(LordToil_PanicFlee))]
    [HarmonyPatch("Init")]
    public static class VanillaMemesExpanded_LordToil_PanicFlee_Init_Patch
    {
        [HarmonyPostfix]
        static void UndraftWhenEnemyFlees()
        {

            if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Violence_Abhorrent) != null)
            { 
                foreach(Pawn pawn in Find.CurrentMap.mapPawns.FreeColonistsAndPrisoners)
                {
                    if (pawn.drafter?.Drafted == true && pawn.Ideo?.HasPrecept(InternalDefOf.VME_Violence_Abhorrent)==true)
                    {
                        pawn.drafter.Drafted = false;
                    }
                }
            
            
            
            
            }





         }
    }








}
