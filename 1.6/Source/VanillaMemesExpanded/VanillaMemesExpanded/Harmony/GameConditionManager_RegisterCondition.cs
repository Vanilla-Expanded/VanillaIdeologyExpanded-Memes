﻿using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System;
using Verse.AI;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(GameConditionManager))]
    [HarmonyPatch("RegisterCondition")]
    public static class VanillaMemesExpanded_GameConditionManager_RegisterCondition_Patch
    {
        [HarmonyPostfix]
        static void SendRandomMood(GameCondition cond)
        {
            if (cond?.def == GameConditionDefOf.Eclipse)
            {
                foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_Colonists.InRandomOrder())
                {
                    System.Random random = new System.Random(Current.Game.tickManager.TicksAbs + PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_Colonists.IndexOf(pawn));
                    int randomMood = random.Next(0, 9);
                    WorldComponent_MoodTracker.Instance.AddColonistAndRandomMood(pawn, randomMood);
                }
            }
        }
    }
}









