using System;
using RimWorld;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class WorldComponent_AlcoholScarsAndSlaveryTracker : WorldComponent
    {



        public int tickCounter = tickInterval;
        public const int tickInterval = 15000;
        public int ticksWithoutADrink;
        public Dictionary<Pawn, int> colonist_booze_tracker = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;
       
        public static WorldComponent_AlcoholScarsAndSlaveryTracker Instance;

        public WorldComponent_AlcoholScarsAndSlaveryTracker(World world) : base(world) => Instance = this;

      
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterBoozeAndScars", 0, true);
            Scribe_Collections.Look(ref colonist_booze_tracker, "colonist_booze_tracker", LookMode.Reference, LookMode.Value, ref list2, ref list3);
        }

        public void AddColonistToBoozeList(Pawn pawn, int ticks)
        {
            if (pawn != null && !colonist_booze_tracker.ContainsKey(pawn))
            {
                colonist_booze_tracker[pawn] = ticks;
            }

        }

        public void IncreasePawnBoozeTicks(Pawn pawn, int ticks)
        {
            if (pawn != null)
            {
                colonist_booze_tracker[pawn] += ticks;
            }
        }
        public void ResetPawnBoozeTicks(Pawn pawn)
        {
            if (pawn != null)
            {
                colonist_booze_tracker[pawn] = 0;
            }
        }


        public override void WorldComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Alcohol_Demanded) != null||
                    Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Alcohol_MildAbstinence) != null)
                {
                    foreach (Pawn p in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                    {
                        AddColonistToBoozeList(p, 0);

                        if (colonist_booze_tracker[p] < int.MaxValue - tickInterval)
                        {
                            IncreasePawnBoozeTicks(p, tickInterval);

                        }



                    }


                }
                
                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Scars_Honorable) != null)
                {

                    foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                    {
                        StaticCollections.AddColonistToScarList(pawn, 0);
                        int realNumberOfScars = 0;
                        foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
                        {
                            if (hediff.IsPermanent() || hediff.def == HediffDefOf.Scarification)
                            {
                                realNumberOfScars++;
                            }
                        }
                        StaticCollections.SetPawnScars(pawn, realNumberOfScars);
                    }

                }

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Slavery_Forbidden) != null)
                {

                    foreach (Pawn pawn in PawnsFinder.AllMaps_SpawnedPawnsInFaction(Faction.OfPlayer))
                    {
                        if (pawn.IsSlave)
                        {
                            StaticCollections.AddToEnslavedPawns(pawn);
                        }
                    }

                }






                tickCounter = 0;
            }



        }


    }


}
