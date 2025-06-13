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
    [HarmonyPatch(typeof(Pawn_RoyaltyTracker))]
    [HarmonyPatch("OnPostTitleChanged")]
    public static class VanillaMemesExpanded_Pawn_RoyaltyTracker_OnPostTitleChanged_Patch
    {
        [HarmonyPostfix]
        public static void RemoveMajordomoRole(Pawn_RoyaltyTracker __instance)
        {
            if ((__instance?.pawn?.Ideo?.HasMeme(InternalDefOf.VME_Royal) == true) && ((Precept_RoleSingle)__instance?.pawn?.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_Majordomo))?.ChosenPawnSingle() == __instance.pawn)
            {
                Precept_RoleSingle precept_role = __instance.pawn?.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_Majordomo) as Precept_RoleSingle;
                precept_role.Unassign(__instance.pawn, false);
            }
        }
    }
}
