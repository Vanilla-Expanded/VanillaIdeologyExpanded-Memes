using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;

using System.Linq;

namespace VanillaMemesExpanded
{
    public class MapComponent_FireAndPenTracker : MapComponent
    {



        public int tickCounter = tickInterval;
        public const int tickInterval = 2000;
   
      
        public MapComponent_FireAndPenTracker(Map map) : base(map)
        {

        }

       
     
        public override void MapComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                if (map.IsPlayerHome)
                {
                    if ((Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Fire_Desired) != null) || (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Fire_Despised) != null))
                    {
                        int firesInTheMap = 0;
                        foreach (string fireSource in StaticCollections.fireSourcesForFirePrecepts)
                        {
                            if (DefDatabase<ThingDef>.GetNamedSilentFail(fireSource) != null)
                            {
                                firesInTheMap += map.listerThings.ThingsOfDef(DefDatabase<ThingDef>.GetNamedSilentFail(fireSource)).Count;
                            }
                        }
             
                        StaticCollections.SetFireInMap(map, firesInTheMap);
                    }

                    if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Ranching_Disliked) != null)

                    {                 
                       
                        int pens = map.listerThings.ThingsOfDef(InternalDefOf.PenMarker).Count;

                        StaticCollections.SetPensInMap(map, pens);

                    }

                    

                }

                tickCounter = 0;
            }

        }

    }

}



