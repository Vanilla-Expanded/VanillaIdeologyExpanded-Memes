using RimWorld;
using Verse;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace VanillaMemesExpanded
{
    public class PlaceWorker_OnWater : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            foreach (IntVec3 c in GenAdj.CellsOccupiedBy(loc, rot, checkingDef.Size))
            {
                if (!map.terrainGrid.TerrainAt(c).IsWater || map.terrainGrid.TerrainAt(c).passability == Traversability.Impassable)
                {
                    return new AcceptanceReport("VME_NeedsWater".Translate());
                }
            }
            foreach (Thing generator in GenRadial.RadialDistinctThingsAround(loc, map, 5, true))
            {
                Building generatorBuilding = generator as Building;
                if (generatorBuilding != null &&generatorBuilding.def.defName.Contains("FishingTrap"))
                { return new AcceptanceReport("VME_NeedsDistance".Translate()); }

                Thing generatorBuildingBlueprint = generator as Thing;
                if (generatorBuildingBlueprint != null && (generatorBuildingBlueprint.def.IsBlueprint || generatorBuildingBlueprint.def.IsFrame) && generatorBuildingBlueprint.def.entityDefToBuild.defName.Contains("FishingTrap"))
                { return new AcceptanceReport("VME_NeedsDistance".Translate()); }

            }



            return true;
        }

        public override void DrawGhost(ThingDef def, IntVec3 loc, Rot4 rot, Color ghostCol, Thing thing = null)
        {

            Color color2 = new Color(0f, 0.6f, 0f);

            GenDraw.DrawRadiusRing(loc, 5, color2);

        }




    }


}


