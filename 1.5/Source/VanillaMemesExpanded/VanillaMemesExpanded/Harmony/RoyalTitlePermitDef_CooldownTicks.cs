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

    [HarmonyPatch(typeof(RoyalTitlePermitDef))]
    [HarmonyPatch("CooldownTicks", MethodType.Getter)]
    public static class VanillaMemesExpanded_RoyalTitlePermitDef_CooldownTicks_Patch
    {
        [HarmonyPostfix]
        static void AdjustPreceptCooldown(ref int __result)
        {
            if (!Find.IdeoManager.classicMode)
            {
                if (Current.Game.World.factionManager.OfPlayer.ideos?.PrimaryIdeo?.HasPrecept(InternalDefOf.VME_PermitCooldown_Increased) == true)
                {
                    __result *= 2;

                }

                if (Current.Game.World.factionManager.OfPlayer.ideos?.PrimaryIdeo?.HasPrecept(InternalDefOf.VME_PermitCooldown_Lowered) == true)
                {
                    __result =(int) (0.7 * __result);

                }
            }
        }
    }

}
