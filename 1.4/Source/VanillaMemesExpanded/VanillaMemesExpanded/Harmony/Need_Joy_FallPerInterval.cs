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


    [HarmonyPatch(typeof(Need_Joy))]
    [HarmonyPatch("FallPerInterval", MethodType.Getter)]
    public static class VanillaMemesExpanded_Need_Joy_FallPerInterval_Patch
    {
        [HarmonyPostfix]
        public static void ModifyFallIntervalForPartyHost(ref Pawn ___pawn, ref float __result)
        {
          

            if (___pawn.Ideo?.HasPrecept(InternalDefOf.VME_IdeoRole_PartyHost)==true){

                Precept_Role precept_role = ___pawn.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_PartyHost) as Precept_Role;
                if(precept_role.ChosenPawnSingle() == ___pawn)
                {
                    __result *= 2;
                }


            }
            if (___pawn.Ideo?.HasPrecept(InternalDefOf.VME_IdeoRole_Nurse) == true)
            {

                Precept_Role precept_role = ___pawn.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_Nurse) as Precept_Role;
                if (precept_role.ChosenPawnSingle() == ___pawn && ___pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VME_MedicalEmergencyHediff)!=null)
                {
                    __result *= 0;
                }


            }





        }
    }








}
