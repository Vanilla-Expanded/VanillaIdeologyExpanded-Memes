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


    [HarmonyPatch(typeof(LordJob_Joinable_Party))]
    [HarmonyPatch("AttendeeThought", MethodType.Getter)]
    public static class VanillaMemesExpanded_LordJob_Joinable_Party_AttendeeThought_Patch
    {
        [HarmonyPostfix]
        static void ChangeThoughtForParty(Pawn ___organizer, ref ThoughtDef __result)
        {

            if (___organizer.Ideo?.GetPrecept(InternalDefOf.VME_Recreation_Loved) != null)
            {

                __result = InternalDefOf.VME_AttendedParty;
            }





        }
    }

    [HarmonyPatch(typeof(LordJob_Joinable_Party))]
    [HarmonyPatch("OrganizerThought", MethodType.Getter)]
    public static class VanillaMemesExpanded_LordJob_Joinable_Party_OrganizerThought_Patch
    {
        [HarmonyPostfix]
        static void ChangeThoughtForPartyOrganizer(Pawn ___organizer, ref ThoughtDef __result)
        {

            if (___organizer.ideo?.Ideo?.GetPrecept(InternalDefOf.VME_Recreation_Loved) != null)
            {

                __result = InternalDefOf.VME_AttendedParty;
            }





        }
    }








}
