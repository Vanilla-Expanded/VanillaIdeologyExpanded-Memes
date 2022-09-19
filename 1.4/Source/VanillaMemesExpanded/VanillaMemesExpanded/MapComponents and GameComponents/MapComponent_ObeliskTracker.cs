using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class MapComponent_ObeliskTracker : MapComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 3000;
        public Dictionary<Pawn, bool> colonist_obelisk_tracker_backup = new Dictionary<Pawn, bool>();
        List<Pawn> list2;
        List<bool> list3;


        public MapComponent_ObeliskTracker(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {
             PawnCollectionClass.colonist_obelisk_tracker = colonist_obelisk_tracker_backup; 
                

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref colonist_obelisk_tracker_backup, "colonist_obelisk_tracker_backup", LookMode.Reference, LookMode.Value, ref list2, ref list3);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterObelisks", 0, true);

        }
        public override void MapComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Corruption_Essential) != null)
                   
                {
                    if (map.IsPlayerHome) {
                        colonist_obelisk_tracker_backup = PawnCollectionClass.colonist_obelisk_tracker;

                        foreach (Pawn pawn in map.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer))
                        {
                            bool obeliskFound = false;
                            int num = GenRadial.NumCellsInRadius(5.9f);
                            for (int i = 0; i < num; i++)
                            {
                                IntVec3 intVec = pawn.Position + GenRadial.RadialPattern[i];
                                if (intVec.InBounds(map) && !intVec.Fogged(map))
                                {
                                    foreach (Thing thing in intVec.GetThingList(map))
                                    {
                                        Building building;
                                        if ((building = (thing as Building)) != null && building.def.defName=="VME_Obelisk")
                                        {
                                            obeliskFound = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            PawnCollectionClass.AddColonistAndObelisk(pawn, obeliskFound);
                           


                        }

                    }
                    


                }

                tickCounter = 0;
            }



        }
       



    }


}



