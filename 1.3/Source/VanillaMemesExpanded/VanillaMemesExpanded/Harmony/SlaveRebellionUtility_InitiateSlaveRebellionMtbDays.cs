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


    [HarmonyPatch(typeof(SlaveRebellionUtility))]
    [HarmonyPatch("InitiateSlaveRebellionMtbDays")]
    public static class VanillaMemesExpanded_SlaveRebellionUtility_InitiateSlaveRebellionMtbDays_Patch
    {
        [HarmonyPostfix]
        static void SetCorruptedSlaveToMinusOne(Pawn pawn,ref float __result)
        {
           
            if (pawn.needs != null)
            {
                Need_Corruption need = pawn.needs.TryGetNeed<Need_Corruption>();
                if (need?.ShowOnNeedList==true&&need?.CurLevel>0)
                {
                    __result= - 1f;
                }
            }





            }
    }








}
