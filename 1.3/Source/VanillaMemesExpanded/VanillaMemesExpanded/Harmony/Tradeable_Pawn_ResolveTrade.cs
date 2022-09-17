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


    [HarmonyPatch(typeof(Tradeable_Pawn))]
    [HarmonyPatch("ResolveTrade")]
    public static class VanillaMemesExpanded_Tradeable_Pawn_ResolveTrade_Patch
    {
        [HarmonyPostfix]
        static void DetectSlaveBought(Tradeable_Pawn __instance)
        {

            if (__instance.ActionToDo == TradeAction.PlayerBuys)
            {
                Pawn AnyPawn = (Pawn)__instance.AnyThing;

                if (AnyPawn.RaceProps?.Humanlike ==true)
                {
                    Find.HistoryEventsManager.RecordEvent(new HistoryEvent(InternalDefOf.VME_BoughtSlave));
                }

            }







        }
    }








}
