using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using Verse.AI.Group;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(LordToil_PanicFlee))]
    [HarmonyPatch("Init")]
    public static class VanillaMemesExpanded_LordToil_PanicFlee_Init_Patch
    {
        [HarmonyPostfix]
        static void UndraftWhenEnemyFlees(Lord ___lord)
        {
            if (Find.IdeoManager.classicMode) return;
            if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Violence_Abhorrent) != null)
            {
                // Create a new list as otherwise we'll be met with `Collection was modified; enumeration operation may not execute.`
                foreach (Pawn pawn in new List<Pawn>(___lord.Map.mapPawns.FreeColonistsAndPrisoners))
                {
                    if (pawn.drafter?.Drafted == true && pawn.Ideo?.HasPrecept(InternalDefOf.VME_Violence_Abhorrent) == true)
                    {
                        pawn.drafter.Drafted = false;
                    }
                }
            }
        }
    }
}
