using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Collections;


namespace VanillaMemesExpanded
{
    public class WorldComponent_BestCrafterTracker : WorldComponent
    {

       
       
        public int tickCounter = tickInterval;
        public const int tickInterval = 6000;
        public Pawn currentBestLeaderPawn;

        public static WorldComponent_BestCrafterTracker Instance;

        public WorldComponent_BestCrafterTracker(World world) : base(world) => Instance = this;

        public override void FinalizeInit()
        {

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look<Pawn>(ref this.currentBestLeaderPawn, "currentBestLeaderPawn");
        }

        public override void WorldComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if (tickCounter > tickInterval)
            {
               
                Ideo ideo = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo;
                if (ideo?.HasPrecept(InternalDefOf.VME_Leader_BestCrafter) == true)
                {
                   
                    Pawn mostSkilledPawn = null;
                    int highestSkillLevel = 0;

                    foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                    {
                        if (!pawn.IsGhoul&&pawn.skills.GetSkill(SkillDefOf.Crafting).Level > highestSkillLevel && pawn.ideo?.Ideo == ideo &&!pawn.IsSlave)
                        {
                            highestSkillLevel = pawn.skills.GetSkill(SkillDefOf.Crafting).Level;
                            mostSkilledPawn = pawn;
                        }
                    }

                    Precept_Role precept_role = mostSkilledPawn?.Ideo?.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role;
                    Pawn currentPawn = precept_role?.ChosenPawnSingle();
                
                    if (currentPawn != mostSkilledPawn || currentPawn?.skills.GetSkill(SkillDefOf.Crafting).Level< highestSkillLevel)
                    {
                      
                        currentBestLeaderPawn = mostSkilledPawn;
                        if (precept_role.RequirementsMet(mostSkilledPawn)) {
                            precept_role.Unassign(precept_role.ChosenPawnSingle(), false);
                            precept_role.Assign(mostSkilledPawn, true);
                        }
                       
                    }

                }

                tickCounter = 0;
            }



        }


    }


}
