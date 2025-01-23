using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using RimWorld.Planet;


namespace VanillaMemesExpanded
{
    public class WorldComponent_BestPsycasterAndTitleTracker : WorldComponent
    {

       
       
        public int tickCounter = tickInterval;
        public const int tickInterval = 6000;
        public Pawn currentPsycasterLeaderPawn;
        public Pawn currentTitleLeaderPawn;

        public static WorldComponent_BestPsycasterAndTitleTracker Instance;

        public WorldComponent_BestPsycasterAndTitleTracker(World world) : base(world) => Instance = this;


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look<Pawn>(ref this.currentPsycasterLeaderPawn, "currentPsycasterLeaderPawn");
            Scribe_References.Look<Pawn>(ref this.currentTitleLeaderPawn, "currentTitleLeaderPawn");

        }

        public override void WorldComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (ModsConfig.RoyaltyActive)
                {
                    Ideo ideo = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo;
                    if (ideo?.HasPrecept(InternalDefOf.VME_Leader_BestPsycaster)==true)
                    {
                        Pawn mostSkilledPawn = null;
                        int highestSkillLevel = 0;

                        foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                        {
                            if (pawn?.IsGhoul==false && pawn.GetPsylinkLevel() > highestSkillLevel && pawn.ideo?.Ideo == ideo && !pawn.IsSlave)
                            {
                                highestSkillLevel = pawn.GetPsylinkLevel();
                                mostSkilledPawn = pawn;
                            }
                        }

                        Precept_Role precept_role = mostSkilledPawn?.Ideo?.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role;
                        Pawn currentPawn = precept_role?.ChosenPawnSingle();


                        if (currentPawn != mostSkilledPawn || currentPawn?.GetPsylinkLevel() < highestSkillLevel)
                        {
                            currentPsycasterLeaderPawn = mostSkilledPawn;
                            if (precept_role.RequirementsMet(mostSkilledPawn)) {
                                precept_role.Unassign(precept_role.ChosenPawnSingle(), false);
                                precept_role.Assign(mostSkilledPawn, true);
                            }
                                
                        }

                    }

                    if (ideo?.HasPrecept(InternalDefOf.VME_Leader_HighestTitle) == true)
                    {
                        Pawn highestTitlePawn = null;
                        int highestTitle = 0;

                        foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                        {
                            if (pawn.royalty?.MainTitle()?.seniority > highestTitle && pawn.ideo?.Ideo == ideo && !pawn.IsSlave)
                            {
                                highestTitle = pawn.royalty.MainTitle().seniority;
                                highestTitlePawn = pawn;
                            }
                        }

                        Precept_Role precept_role = highestTitlePawn?.Ideo?.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role;
                        Pawn currentPawn = precept_role?.ChosenPawnSingle();


                        if (currentPawn != highestTitlePawn || currentPawn?.royalty?.MainTitle()?.seniority < highestTitle)
                        {
                            currentTitleLeaderPawn = highestTitlePawn;
                            if (precept_role.RequirementsMet(highestTitlePawn))
                            {
                                precept_role.Unassign(precept_role.ChosenPawnSingle(), false);
                                precept_role.Assign(highestTitlePawn, true);
                            }

                        }

                    }

                }

                tickCounter = 0;
            }



        }


    }


}
