using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class GameComponent_BestPsycasterAndTitleTracker : GameComponent
    {

       
       
        public int tickCounter = 0;
        public int tickInterval = 4000;


        public GameComponent_BestPsycasterAndTitleTracker(Game game) : base()
        {

        }

        public override void FinalizeInit()
        {

            base.FinalizeInit();

        }

      

        public override void GameComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (ModsConfig.RoyaltyActive)
                {
                    Ideo ideo = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo;
                    if (ideo?.HasPrecept(DefDatabase<PreceptDef>.GetNamedSilentFail("VME_Leader_BestPsycaster"))==true)
                    {
                        Pawn mostSkilledPawn = null;
                        int highestSkillLevel = 0;

                        foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                        {
                            if (!pawn.IsGhoul&&PawnUtility.GetPsylinkLevel(pawn) > highestSkillLevel && pawn.ideo?.Ideo == ideo && !pawn.IsSlave)
                            {
                                highestSkillLevel = PawnUtility.GetPsylinkLevel(pawn);
                                mostSkilledPawn = pawn;
                            }
                        }

                        Precept_Role precept_role = mostSkilledPawn?.Ideo?.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role;

                        if (precept_role?.ChosenPawnSingle() != mostSkilledPawn)
                        {
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

                        if (precept_role?.ChosenPawnSingle() != highestTitlePawn)
                        {
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
