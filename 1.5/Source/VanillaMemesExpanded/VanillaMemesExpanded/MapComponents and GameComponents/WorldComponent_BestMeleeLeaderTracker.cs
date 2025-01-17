using System;
using RimWorld;
using Verse;
using RimWorld.Planet;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class WorldComponent_BestMeleeLeaderTracker : WorldComponent
    {

       
       
        public int tickCounter = tickInterval;
        public const int tickInterval = 6000;
        public Pawn mostSkilledPawn;
        public Pawn currentBestMeleeLeaderPawn;


        public static WorldComponent_BestMeleeLeaderTracker Instance;

        public WorldComponent_BestMeleeLeaderTracker(World world) : base(world) => Instance = this;


       

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_References.Look<Pawn>(ref this.mostSkilledPawn, "mostSkilledPawn");
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterMelee", 0, true);
            Scribe_References.Look<Pawn>(ref this.currentBestMeleeLeaderPawn, "currentBestMeleeLeaderPawn");

        }

        public override void WorldComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                Ideo ideo = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo;
                if (ideo?.HasPrecept(InternalDefOf.VME_Leader_BestFighter)==true)
                {

                    int highestSkillLevel = 0;

                    foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                    {
                        if (!pawn.IsGhoul && pawn.skills.GetSkill(SkillDefOf.Melee).Level > highestSkillLevel && pawn.ideo.Ideo == ideo && !pawn.IsSlave)
                        {
                            highestSkillLevel = pawn.skills.GetSkill(SkillDefOf.Melee).Level;
                            mostSkilledPawn = pawn;
                        }
                    }

                    Precept_Role precept_role = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role;
                    Pawn leader = precept_role.ChosenPawnSingle();

                    if (leader == null)
                    {
                        currentBestMeleeLeaderPawn = mostSkilledPawn;
                        if (precept_role.RequirementsMet(mostSkilledPawn))
                        { precept_role.Assign(mostSkilledPawn, true); }


                    }


                  

                }
            

                tickCounter = 0;
            }



        }


    }


}
