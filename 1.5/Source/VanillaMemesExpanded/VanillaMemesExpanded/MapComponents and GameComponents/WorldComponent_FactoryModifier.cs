using System;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace VanillaMemesExpanded
{
    public class WorldComponent_FactoryModifier : WorldComponent
    {

        public int tickCounter = tickInterval;
        public const int tickInterval = 20000;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterFactories", 0, true);       
        }


        public WorldComponent_FactoryModifier(World world) : base(world)
        {

        }

        public override void WorldComponentTick()
        {

            if (Find.IdeoManager.classicMode) return;
            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_AutomationEfficiency_Increased) != null)
                {
                    ItemProcessor.FactoryMultiplierClass.FactoryPreceptMultiplier = 0.75f;
                }
                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_AutomationEfficiency_Decreased) != null)
                {
                    ItemProcessor.FactoryMultiplierClass.FactoryPreceptMultiplier = 1.5f;
                }
                tickCounter = 0;
            }

        }

    }

}
