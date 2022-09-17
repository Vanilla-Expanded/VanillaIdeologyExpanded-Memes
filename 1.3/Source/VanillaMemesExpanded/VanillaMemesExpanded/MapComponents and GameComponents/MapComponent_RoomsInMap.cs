using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class MapComponent_RoomsInMap : MapComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 6000;
        public int roomsInMap_backup;
        public int hospitalTilesInMap_backup;
        public bool hospitalDirty_backup = false;


        public MapComponent_RoomsInMap(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {
            if (map.IsPlayerHome)
            {
                PawnCollectionClass.roomsInMap = roomsInMap_backup;
                PawnCollectionClass.hospitalTilesInMap = hospitalTilesInMap_backup;
                PawnCollectionClass.hospitalDirty = hospitalDirty_backup;
            }

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.roomsInMap_backup, "roomsInMap_backup", 0, true);
            Scribe_Values.Look<int>(ref this.hospitalTilesInMap_backup, "hospitalTilesInMap_backup", 0, true);
            Scribe_Values.Look<bool>(ref this.hospitalDirty_backup, "hospitalDirty_backup", false, true);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterRooms", 0, true);

        }
        public override void MapComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                if (map.IsPlayerHome && Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_PermanentBases_Desired) != null) {
                    roomsInMap_backup = PawnCollectionClass.roomsInMap;

                    int totalRooms = 0;

                    foreach (Room room in map.regionGrid.allRooms)
                    {
                        if (room.CellCount > 24)
                        {
                           
                            totalRooms++;
                        }
                    }
                    roomsInMap_backup = totalRooms;
                    PawnCollectionClass.roomsInMap = totalRooms;
                }

                if (map.IsPlayerHome && Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Hospital_Required) != null)
                {
                    hospitalTilesInMap_backup = PawnCollectionClass.hospitalTilesInMap;

                    int totalHospitalTiles = 0;

                    foreach (Room room in map.regionGrid.allRooms)
                    {
                                           
                        if (room.Role == RoomRoleDefOf.Hospital)
                        {
                            int cleanStageIndex = RoomStatDefOf.Cleanliness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Cleanliness));
                            int scoreStageIndex = RoomStatDefOf.Impressiveness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Impressiveness));

                            if (cleanStageIndex < 3)
                            {
                                hospitalDirty_backup = true;
                            }else hospitalDirty_backup = false;

                            if (scoreStageIndex >= 3) { 
                                totalHospitalTiles += room.CellCount;
                            }
                        }



                    }
                    hospitalTilesInMap_backup = totalHospitalTiles;
                    PawnCollectionClass.hospitalTilesInMap = totalHospitalTiles;
                    PawnCollectionClass.hospitalDirty = hospitalDirty_backup;
                }



                tickCounter = 0;
            }



        }
       



    }


}



