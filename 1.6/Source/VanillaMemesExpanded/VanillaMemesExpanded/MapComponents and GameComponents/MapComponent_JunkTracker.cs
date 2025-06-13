﻿using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace VanillaMemesExpanded
{
    public class MapComponent_JunkTracker : MapComponent
    {



        public int tickCounter = tickInterval;
        public const int tickInterval = 12000;
        public Dictionary<Pawn, int> colonist_junk_tracker_backup = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;


        public MapComponent_JunkTracker(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {
             StaticCollections.colonist_junk_tracker = colonist_junk_tracker_backup; 
                

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref colonist_junk_tracker_backup, "colonist_junk_tracker_backup", LookMode.Reference, LookMode.Value, ref list2, ref list3);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterJunk", 0, true);

        }
        public override void MapComponentTick()
        {

            if (Find.IdeoManager.classicMode) return;
            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Junk_Beautiful) != null|| Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Junk_Preferred) != null)
                   
                {

                    colonist_junk_tracker_backup = StaticCollections.colonist_junk_tracker;

                    foreach (Pawn pawn in map.mapPawns.FreeColonistsSpawned)
                    {
                        int totalNumberOfJunk = 0;
                        int num = GenRadial.NumCellsInRadius(20.9f);
                        for (int i = 0; i < num; i++)
                        {
                            IntVec3 intVec = pawn.Position + GenRadial.RadialPattern[i];
                            if (intVec.InBounds(map) && !intVec.Fogged(map))
                            {
                                foreach (Thing thing in intVec.GetThingList(map))
                                {
                                    Building building;
                                    if ((building = (thing as Building)) != null && (building.def.defName.Contains("Ancient")|| building.def.defName.Contains("ancient")))
                                    {
                                        totalNumberOfJunk++;
                                    }
                                }
                            }
                        }
                        StaticCollections.AddColonistToJunkList(pawn, totalNumberOfJunk);
                        StaticCollections.SetPawnJunk(pawn, totalNumberOfJunk);


                    }


                }
                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_BookQuality_Desired) != null)
                {
                    int legendaryBooksInTheMap = 0;

                    List<Thing> booksInMap = map.listerThings.AllThings.Where(x => x.def.thingCategories?.Contains(InternalDefOf.Books) == true).ToList();
                    foreach (Thing book in booksInMap)
                    {
                        CompQuality quality = book.TryGetComp<CompQuality>();
                        if(quality?.Quality == QualityCategory.Legendary)
                        {
                            legendaryBooksInTheMap++;
                        }

                    }


                    StaticCollections.SetLegendaryBooksInMap(map, legendaryBooksInTheMap);
                }

                tickCounter = 0;
            }



        }
       



    }


}



