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


    [HarmonyPatch(typeof(InteractionUtility))]
    [HarmonyPatch("CanReceiveInteraction")]
    public static class VanillaMemesExpanded_InteractionUtility_CanReceiveInteraction_Patch
    {
        [HarmonyPostfix]
        static void DontInteract(Pawn pawn, ref bool __result)
        {
           if(pawn.Ideo?.HasPrecept(InternalDefOf.VME_SocialInteractions_Disallowed) == true &&!pawn.IsPrisoner)
            {
                __result = false;
            }

        }
    }








}
