using HarmonyLib;
using RimWorld;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection.Emit;

namespace VanillaMemesExpanded
{
    [HarmonyPatch(typeof(IncidentWorker))]
    [HarmonyPatch("CanFireNow")]
    public static class VanillaMemesExpanded_IncidentWorker_CanFireNow_Patch
    {
       

        [HarmonyPrefix]
        static bool DisableIncident(IncidentWorker __instance, IncidentParms parms, ref bool __result)
        {
            if (parms.forced) return true;
            if (Faction.OfPlayer.ideos.PrimaryIdeo is not { } ideo) return true;
            if (__instance.def != IncidentDefOf.WandererJoin && __instance.def != InternalDefOf.WandererJoinAbasia) return true;
            if (ideo.PreceptsListForReading.SelectMany(precept => precept.def.comps).OfType<PreceptComp_DisableIncident>()
                .Any(disableIncident => disableIncident.Incident == __instance.def)) return __result = false;

            return true;
        }
    }
}