using System;
using RimWorld;
using Verse;

using Verse.AI;
using Verse.AI.Group;



namespace VanillaMemesExpanded
{
    public class MapComponent_MechanoidRaidFleeing : MapComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 20000;



        public MapComponent_MechanoidRaidFleeing(Map map) : base(map)
        {

        }



        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterMechs", 0, true);


        }
        public override void MapComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                
                if (Current.Game.World.factionManager.OfPlayer.ideos.HasAnyIdeoWithMeme(InternalDefOf.VME_MechanoidSupremacy))

                {
                   
                    int numberOfEffigies = map.listerThings.ThingsOfDef(InternalDefOf.VME_MechanoidEffigy).Count;
                   
                    if (numberOfEffigies >= PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists.Count*2)
                    {
                      
                        foreach (Lord lord in Find.CurrentMap.lordManager.lords)
                        {
                            
                            if (lord.faction == Faction.OfMechanoids)
                            {
                               
                                for (int i = 0; i < lord.ownedPawns.Count; i++)
                                {
                                    Pawn pawn = lord.ownedPawns[i];
                                   
                                    pawn.mindState.duty = new PawnDuty(DutyDefOf.ExitMapRandom);
                                    
                                }
                            }
                        }


                    }
                }

                tickCounter = 0;
            }








        }




    }


}



