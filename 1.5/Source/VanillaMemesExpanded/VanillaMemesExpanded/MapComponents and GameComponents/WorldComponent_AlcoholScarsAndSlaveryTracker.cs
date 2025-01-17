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
        public Dictionary<Pawn, int> colonist_booze_tracker_backup = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;
       

        public WorldComponent_AlcoholScarsAndSlaveryTracker(World world) : base(world)
        {

        }

        public override void FinalizeInit()
        {

            PawnCollectionClass.colonist_booze_tracker = colonist_booze_tracker_backup;
           


            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterBoozeAndScars", 0, true);
            Scribe_Collections.Look(ref colonist_booze_tracker_backup, "colonist_booze_tracker_backup", LookMode.Reference, LookMode.Value, ref list2, ref list3);

        }


        public override void WorldComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                colonist_booze_tracker_backup = PawnCollectionClass.colonist_booze_tracker;
                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Alcohol_Demanded) != null||
                    Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Alcohol_MildAbstinence) != null)
                {
                    foreach (Pawn p in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                    {
                        PawnCollectionClass.AddColonistToBoozeList(p, 0);

                        if (PawnCollectionClass.colonist_booze_tracker[p] < int.MaxValue - tickInterval)
                        {
                            PawnCollectionClass.IncreasePawnBoozeTicks(p, tickInterval);

                        }



                    }


                }
                
                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Scars_Honorable) != null)
                {

                    foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                    {
                        PawnCollectionClass.AddColonistToScarList(pawn, 0);
                        int realNumberOfScars = 0;
                        foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
                        {
                            if (hediff.IsPermanent() || hediff.def == HediffDefOf.Scarification)
                            {
                                realNumberOfScars++;
                            }
                        }
                        PawnCollectionClass.SetPawnScars(pawn, realNumberOfScars);
                    }

                }

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Slavery_Forbidden) != null)
                {

                    foreach (Pawn pawn in PawnsFinder.AllMaps_SpawnedPawnsInFaction(Faction.OfPlayer))
                    {
                        if (pawn.IsSlave)
                        {
                            PawnCollectionClass.AddToEnslavedPawns(pawn);
                        }
                    }

                }






                tickCounter = 0;
            }



        }


    }


}
