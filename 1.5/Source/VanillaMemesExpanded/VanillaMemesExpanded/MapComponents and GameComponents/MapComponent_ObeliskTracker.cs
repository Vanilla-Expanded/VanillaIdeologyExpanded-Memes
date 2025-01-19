using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class MapComponent_ObeliskTracker : MapComponent
    {



        public int tickCounter = tickInterval;
        public const int tickInterval = 3000;
        public Dictionary<Pawn, bool> colonist_obelisk_tracker_backup = new Dictionary<Pawn, bool>();
        public HashSet<Thing> obelisks_InMap = new HashSet<Thing>();
        List<Pawn> list2;
        List<bool> list3;


        public MapComponent_ObeliskTracker(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {
            StaticCollections.colonist_obelisk_tracker = colonist_obelisk_tracker_backup;


            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref colonist_obelisk_tracker_backup, "colonist_obelisk_tracker_backup", LookMode.Reference, LookMode.Value, ref list2, ref list3);
            Scribe_Collections.Look(ref obelisks_InMap, "obelisks_InMap", LookMode.Reference);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterObelisks", 0, true);

        }
        public override void MapComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Corruption_Essential) != null)

                {
                    if (map.IsPlayerHome)
                    {
                        colonist_obelisk_tracker_backup = StaticCollections.colonist_obelisk_tracker;


                        foreach (Pawn pawn in map.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer))
                        {
                            bool obeliskFound = false;

                            if (obelisks_InMap.Count > 0)
                            {
                                foreach (Thing obelisk in obelisks_InMap)
                                {
                                    if (pawn.Position.DistanceTo(obelisk.Position) < 10)
                                    {
                                        obeliskFound = true;
                                        break;
                                    }

                                }
                            }

                            StaticCollections.AddColonistAndObelisk(pawn, obeliskFound);


                        }

                    }



                }

                tickCounter = 0;
            }



        }

        public void AddObeliskToMap(Thing thing)
        {
            if (!obelisks_InMap.Contains(thing))
            {
                obelisks_InMap.Add(thing);
            }
        }

        public void RemoveObeliskFromMap(Thing thing)
        {
            if (obelisks_InMap.Contains(thing))
            {
                obelisks_InMap.Remove(thing);
            }

        }


    }


}



