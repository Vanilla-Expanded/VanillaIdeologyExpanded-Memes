using System;
using RimWorld;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class WorldComponent_TravellingAndTradingTracker : WorldComponent
    {

       
       
        public int tickCounter = tickInterval;
        public const int tickInterval = 6000;
        public int ticksWithoutAbandoning;
        public int ticksWithoutTrading;

        public Dictionary<Pawn, int> colonist_caravan_tracker = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;

        public static WorldComponent_TravellingAndTradingTracker Instance;

        public WorldComponent_TravellingAndTradingTracker(World world) : base(world) => Instance = this;

        

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterTravel", 0, true);
            Scribe_Values.Look<int>(ref this.ticksWithoutAbandoning, "ticksWithoutAbandoning", 0, true);
            Scribe_Collections.Look(ref colonist_caravan_tracker, "colonist_caravan_tracker", LookMode.Reference, LookMode.Value, ref list2, ref list3);
            Scribe_Values.Look<int>(ref this.ticksWithoutTrading, "ticksWithoutTrading", 0, true);

        }


        public override void WorldComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {
               

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_PermanentBases_Despised) != null)
                {

                    int num = 0;
                    List<Map> maps = Find.Maps;
                    for (int i = 0; i < maps.Count; i++)
                    {
                        if (maps[i].IsPlayerHome && maps[i].Parent is Settlement)
                        {
                            num++;
                        }
                    }
                    if (num != 0) {
                        if (ticksWithoutAbandoning < int.MaxValue - tickInterval)
                        {
                            ticksWithoutAbandoning += tickInterval;
                        }

                    } else {
                        if (ticksWithoutAbandoning - tickInterval > 0)
                        {
                            ticksWithoutAbandoning -= tickInterval;
                        } 
                    }

                }
                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Travel_Desired) != null
                    || Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Travel_Despised) != null)

                    
                {
                    List<Pawn> listPawns = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists;
                    foreach (Pawn p in listPawns)
                    {
                        if (p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Travel_Desired) ==true || p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Travel_Despised) ==true)
                        {

                            if (colonist_caravan_tracker.ContainsKey(p) && p.GetCaravan()==null)
                            {
                                if (colonist_caravan_tracker[p] < int.MaxValue - tickInterval)
                                {
                                    IncreasePawnCaravanTicks(p, tickInterval);

                                }
                            }
                        }
                    }
                }
                

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Trading_Required) != null)
                {
                    if (ticksWithoutTrading < int.MaxValue - tickInterval)
                    {
                        ticksWithoutTrading += tickInterval;
                    }
                }



                tickCounter = 0;
            }



        }

        public void IncreasePawnCaravanTicks(Pawn pawn, int ticks)
        {
            if (pawn != null)
            {
                colonist_caravan_tracker[pawn] += ticks;
            }
        }

        public void AddColonistToCaravanList(Pawn pawn, int ticks)
        {
            if (pawn != null && !colonist_caravan_tracker.ContainsKey(pawn))
            {
                colonist_caravan_tracker[pawn] = ticks;
            }
        }


        public void ResetPawnCaravanTicks(Pawn pawn)
        {
            if (pawn != null)
            {
                colonist_caravan_tracker[pawn] = 0;
            }
        }


    }


}
