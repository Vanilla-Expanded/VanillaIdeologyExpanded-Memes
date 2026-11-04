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
            // sometimes log spams NullReferenceException, this prevents loading corrupt obelisk entries
            try
            {
                StaticCollections.colonist_obelisk_tracker = colonist_obelisk_tracker_backup;
            }
            catch (NullReferenceException)
            {
                Log.Warning("Vanilla Memes Expanded - Cleared corrupt colonist_obelisk_tracker");
                StaticCollections.colonist_obelisk_tracker = new Dictionary<Pawn, bool>();
            }


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
                                    try
                                    {
                                        if (pawn.Position.DistanceTo(obelisk.Position) < 10)
                                        {
                                            obeliskFound = true;
                                            break;
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        // we still think there's a valid obelisk that was improperly
                                        // deleted by another mod or corrupted during save/load
                                        // so remove it from the list
                                        RemoveObeliskFromMap(obelisk);
                                        
                                        Log.Warning("Vanilla Memes Expanded - Removed corrupt obelisk from colonist_obelisk_tracker");
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



