using System;
using RimWorld;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class GameComponent_BestMeleeLeaderTracker : GameComponent
    {

       
       
        public int tickCounter = 0;
        public int tickInterval = 3000;
        public Pawn mostSkilledPawn;
        public Pawn pawnThatIsTheLeaderNow;

        public GameComponent_BestMeleeLeaderTracker(Game game) : base()
        {

        }

        public override void FinalizeInit()
        {
            PawnCollectionClass.pawnThatIsTheLeaderNow = pawnThatIsTheLeaderNow;
            PawnCollectionClass.mostSkilledPawn = mostSkilledPawn;

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_References.Look<Pawn>(ref this.mostSkilledPawn, "mostSkilledPawn");
            Scribe_References.Look<Pawn>(ref this.pawnThatIsTheLeaderNow, "pawnThatIsTheLeaderNow");
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterMelee", 0, true);



        }


        public override void GameComponentTick()
        {

          
            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                Ideo ideo = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo;
                if (ideo.HasPrecept(InternalDefOf.VME_Leader_BestFighter))
                {
                    pawnThatIsTheLeaderNow = PawnCollectionClass.pawnThatIsTheLeaderNow;
                    mostSkilledPawn = PawnCollectionClass.mostSkilledPawn;

                    int highestSkillLevel = 0;

                    foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                    {
                        if (pawn.skills.GetSkill(SkillDefOf.Melee).Level > highestSkillLevel && pawn.ideo.Ideo == ideo && !pawn.IsSlave)
                        {
                            highestSkillLevel = pawn.skills.GetSkill(SkillDefOf.Melee).Level;
                            mostSkilledPawn = pawn;
                            PawnCollectionClass.mostSkilledPawn = pawn;
                        }
                    }

                    Precept_Role precept_role = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role;
                    Pawn leader = precept_role.ChosenPawnSingle();

                    if (leader == null)
                    {
                        pawnThatIsTheLeaderNow = mostSkilledPawn;
                        PawnCollectionClass.pawnThatIsTheLeaderNow = pawnThatIsTheLeaderNow;
                        if (precept_role.RequirementsMet(mostSkilledPawn))
                        { precept_role.Assign(mostSkilledPawn, true); }
                            

                    }

                }
                


                tickCounter = 0;
            }



        }


    }


}
