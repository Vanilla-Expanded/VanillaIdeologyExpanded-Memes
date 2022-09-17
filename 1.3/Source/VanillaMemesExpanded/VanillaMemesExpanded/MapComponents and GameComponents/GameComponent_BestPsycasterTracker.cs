using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class GameComponent_BestPsycasterTracker : GameComponent
    {

       
       
        public int tickCounter = 0;
        public int tickInterval = 4000;


        public GameComponent_BestPsycasterTracker(Game game) : base()
        {

        }

        public override void FinalizeInit()
        {

            base.FinalizeInit();

        }

      

        public override void GameComponentTick()
        {

          
            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (ModsConfig.RoyaltyActive)
                {
                    Ideo ideo = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo;
                    if (ideo.HasPrecept(DefDatabase<PreceptDef>.GetNamedSilentFail("VME_Leader_BestPsycaster")))
                    {
                        Pawn mostSkilledPawn = null;
                        int highestSkillLevel = 0;

                        foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                        {
                            if (PawnUtility.GetPsylinkLevel(pawn) > highestSkillLevel && pawn.ideo?.Ideo == ideo && !pawn.IsSlave)
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
                }

                tickCounter = 0;
            }



        }


    }


}
