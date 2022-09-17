using System;
using RimWorld;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class GameComponent_AlcoholScarsAndSlaveryTracker : GameComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 15000;
        public int ticksWithoutADrink;
        public Dictionary<Pawn, int> colonist_booze_tracker_backup = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;
        public Dictionary<Pawn, int> colonist_scar_counter_backup = new Dictionary<Pawn, int>();
        List<Pawn> list4;
        List<int> list5;
        public List<Pawn> enslavedPawns_backup = new List<Pawn>();


        public GameComponent_AlcoholScarsAndSlaveryTracker(Game game) : base()
        {

        }

        public override void FinalizeInit()
        {

            PawnCollectionClass.colonist_booze_tracker = colonist_booze_tracker_backup;
            PawnCollectionClass.colonist_scar_counter = colonist_scar_counter_backup;
            PawnCollectionClass.enslavedPawns = enslavedPawns_backup;


            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterBoozeAndScars", 0, true);
            Scribe_Collections.Look(ref colonist_booze_tracker_backup, "colonist_booze_tracker_backup", LookMode.Reference, LookMode.Value, ref list2, ref list3);
            Scribe_Collections.Look(ref colonist_scar_counter_backup, "colonist_scar_counter_backup", LookMode.Reference, LookMode.Value, ref list4, ref list5);
            Scribe_Collections.Look(ref enslavedPawns_backup, "enslavedPawns_backup", LookMode.Reference);

        }


        public override void GameComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                colonist_scar_counter_backup = PawnCollectionClass.colonist_scar_counter;
                colonist_booze_tracker_backup = PawnCollectionClass.colonist_booze_tracker;
                enslavedPawns_backup = PawnCollectionClass.enslavedPawns;
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
