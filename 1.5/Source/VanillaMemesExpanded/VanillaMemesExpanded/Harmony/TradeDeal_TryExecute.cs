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


    [HarmonyPatch(typeof(TradeDeal))]
    [HarmonyPatch("TryExecute")]
    public static class VanillaMemesExpanded_TradeDeal_TryExecute_Patch
    {
        [HarmonyPostfix]
        static void NotifySuccessfulTrade(bool __result)
        {


            if (__result && !TradeSession.giftMode)
            {
                PawnCollectionClass.ticksWithoutTrading = 0;
            }

            Pawn pawn = TradeSession.playerNegotiator as Pawn;
           // Log.Message(pawn.Name.ToString());
            if (pawn.Ideo?.HasPrecept(InternalDefOf.VME_Anonymity_Required) == true)
            {
                if (pawn.needs != null)
                {
                    Need_Anonymity need = pawn.needs.TryGetNeed<Need_Anonymity>();
                    need.AnonymityTaken(-1);
                    need.CurLevel -= 1;
                   // Log.Message(need.ToString()+" decreased");
                }
            }







        }
    }








}
