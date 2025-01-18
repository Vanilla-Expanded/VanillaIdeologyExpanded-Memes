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
                            }

                            if (scoreStageIndex >= 3) { 
                                
                                hospitalImpressive = true;
                            }
                            totalHospitalTiles += room.CellCount;
                        }



                    }

                    StaticCollections.SetHospitalTilesInMap(map, totalHospitalTiles);
                    StaticCollections.SetHospitalCleanlinessInMap(map, hospitalDirty);
                    StaticCollections.SetHospitalImpressiveInMap(map, hospitalImpressive);

                }

                if (map.IsPlayerHome && Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Library_Required) != null)
                {
                    bool libraryDirty = false;

                    bool libraryImpressive = false;

                    int totalLibraryTiles = 0;

                    foreach (Room room in map.regionGrid.allRooms)
                    {

                        if (room.Role == InternalDefOf.VBE_Library)
                        {
                            int cleanStageIndex = RoomStatDefOf.Cleanliness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Cleanliness));
                            int scoreStageIndex = RoomStatDefOf.Impressiveness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Impressiveness));

                            if (cleanStageIndex < 3)
                            {
                                libraryDirty = true;
                            }
                  

                            if (scoreStageIndex >= 3)
                            {
                                
                                libraryImpressive = true;
                            }
                            totalLibraryTiles += room.CellCount;
                        }



                    }

                    StaticCollections.SetLibraryTilesInMap(map, totalLibraryTiles);
                    StaticCollections.SetLibraryCleanlinessInMap(map, libraryDirty);
                    StaticCollections.SetLibraryImpressiveInMap(map, libraryImpressive);

                }



                tickCounter = 0;
            }



        }
       



    }


}



