using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class MapComponent_RoomsInMap : MapComponent
    {



        public int tickCounter = tickInterval;
        public const int tickInterval = 2000;
        public bool hospitalDirty = false;


        public MapComponent_RoomsInMap(Map map) : base(map)
        {

        }

     

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterRooms", 0, true);

        }
        public override void MapComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {
               
                if (map.IsPlayerHome && Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_PermanentBases_Desired) != null) {
                  

                    int totalRooms = 0;

                    foreach (Room room in map.regionGrid.allRooms)
                    {
                        if (room.CellCount > 24)
                        {
                           
                            totalRooms++;
                        }
                    }
                   
                    StaticCollections.SetRoomInMap(map, totalRooms);
                }

                if (map.IsPlayerHome && Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Hospital_Required) != null)
                {
                    bool hospitalDirty = false;

                    bool hospitalImpressive = false;

                    int totalHospitalTiles = 0;

                    foreach (Room room in map.regionGrid.allRooms)
                    {
                                           
                        if (room.Role == RoomRoleDefOf.Hospital)
                        {
                            int cleanStageIndex = RoomStatDefOf.Cleanliness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Cleanliness));
                            int scoreStageIndex = RoomStatDefOf.Impressiveness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Impressiveness));

                            if (cleanStageIndex < 3)
                            {
                                hospitalDirty = true;
                            }else hospitalDirty = false;

                            if (scoreStageIndex >= 3) { 
                                totalHospitalTiles += room.CellCount;
                                hospitalImpressive = true;
                            }
                        }



                    }

                    StaticCollections.SetHospitalTilesInMap(map, totalHospitalTiles);
                    StaticCollections.SetHospitalCleanlinessInMap(map, hospitalDirty);
                    StaticCollections.SetHospitalImpressiveInMap(map, hospitalImpressive);

                }



                tickCounter = 0;
            }



        }
       



    }


}



