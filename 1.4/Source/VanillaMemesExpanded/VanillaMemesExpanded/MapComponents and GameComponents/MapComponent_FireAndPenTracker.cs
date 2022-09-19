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



        public int tickCounter = 0;
        public int tickInterval = 2000;
        public int firesInTheMap_backup = 0; 
        public int pensInTheMap_backup = 0;
        HashSet<string> allFireSources = new HashSet<string>();


        public MapComponent_FireAndPenTracker(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {
            if (map.IsPlayerHome)
            {
                PawnCollectionClass.firesInTheMap = firesInTheMap_backup;
                PawnCollectionClass.pensInTheMap = pensInTheMap_backup;

            }

            
            HashSet<FireSourcesForPreceptDefs> allLists = DefDatabase<FireSourcesForPreceptDefs>.AllDefsListForReading.ToHashSet();
            foreach (FireSourcesForPreceptDefs individualList in allLists)
            {
                allFireSources.AddRange(individualList.supportedFireSourcesForPrecept);
            }

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.firesInTheMap_backup, "firesInTheMap_backup", 0, true);
            Scribe_Values.Look<int>(ref this.pensInTheMap_backup, "pensInTheMap_backup", 0, true);

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterFire", 0, true);

        }
        public override void MapComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {


                if (map.IsPlayerHome)
                {
                    if ((Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Fire_Desired) != null) || (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Fire_Despised) != null))

                    {

                        firesInTheMap_backup = 0;

                        foreach (string fireSource in allFireSources)
                        {
                            if (DefDatabase<ThingDef>.GetNamedSilentFail(fireSource) != null)
                            {
                                firesInTheMap_backup += map.listerThings.ThingsOfDef(DefDatabase<ThingDef>.GetNamedSilentFail(fireSource)).Count;
                            }

                        }


                       
                        PawnCollectionClass.firesInTheMap = firesInTheMap_backup;


                    }

                    if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Ranching_Disliked) != null)

                    {

                     
                        pensInTheMap_backup = PawnCollectionClass.pensInTheMap;
                        int pens = map.listerThings.ThingsOfDef(DefDatabase<ThingDef>.GetNamedSilentFail("PenMarker")).Count;
                    
                        pensInTheMap_backup = pens;
                      
                        PawnCollectionClass.pensInTheMap = pensInTheMap_backup;


                    }


                   
                }

                


                tickCounter = 0;
            }



        }
       



    }


}



