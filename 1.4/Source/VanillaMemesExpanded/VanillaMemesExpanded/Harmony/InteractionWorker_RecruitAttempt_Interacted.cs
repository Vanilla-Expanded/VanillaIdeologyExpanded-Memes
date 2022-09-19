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


    [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt))]
    [HarmonyPatch("Interacted")]
    public static class VanillaMemesExpanded_InteractionWorker_RecruitAttempt_Interacted_Patch
    {
        [HarmonyPostfix]
        static void DecreaseAnonymity(Pawn initiator)
        {
            if (initiator.Ideo?.HasPrecept(InternalDefOf.VME_Anonymity_Required) == true)
            {
                if (initiator.needs != null)
                {
                    Need_Anonymity need = initiator.needs.TryGetNeed<Need_Anonymity>();
                    need.AnonymityTaken(-0.5f);
                    need.CurLevel -= 0.5f;
                  
                }
            }

        }
    }








}
