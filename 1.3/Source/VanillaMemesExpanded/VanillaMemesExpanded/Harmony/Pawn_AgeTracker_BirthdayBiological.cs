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


    [HarmonyPatch(typeof(Pawn_AgeTracker))]
    [HarmonyPatch("BirthdayBiological")]
    public static class VanillaMemesExpanded_Pawn_AgeTracker_BirthdayBiological_Patch
    {
        [HarmonyPostfix]
        static void IfElderAddTrait(Pawn_AgeTracker __instance, Pawn ___pawn)
        {

            if (___pawn.Ideo?.HasPrecept(InternalDefOf.VME_Elders_Holy)==true)
            {

                if (__instance.AgeBiologicalYears>65 && !___pawn.story.traits.HasTrait(InternalDefOf.VME_Elder))
                {
                    Trait trait = new Trait(InternalDefOf.VME_Elder,0,true);
                    ___pawn.story.traits.GainTrait(trait);



                }

            }
                







        }
    }








}
