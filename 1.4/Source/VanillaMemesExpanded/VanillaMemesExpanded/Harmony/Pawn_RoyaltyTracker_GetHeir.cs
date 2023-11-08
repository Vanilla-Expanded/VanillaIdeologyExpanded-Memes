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


    [HarmonyPatch(typeof(Pawn_RoyaltyTracker))]
    [HarmonyPatch("GetHeir")]
    public static class VanillaMemesExpanded_Pawn_RoyaltyTracker_GetHeir_Patch
    {
        [HarmonyPostfix]
        static void SetHeirToFirstBorn(Pawn_RoyaltyTracker __instance, ref Pawn __result)
        {
            if (__instance.pawn.Ideo?.HasPrecept(InternalDefOf.VME_TitleInheritance_Inherited) == true)
            {
                List<Pawn> children = __instance.pawn.relations.Children.ToList();
                if(children.Count > 0)
                {
                    Pawn heir = (from x in children
                                 orderby x.ageTracker.AgeBiologicalTicks
                                 select x).Last();
                    __result = heir;
                }

            }

        }
    }








}
