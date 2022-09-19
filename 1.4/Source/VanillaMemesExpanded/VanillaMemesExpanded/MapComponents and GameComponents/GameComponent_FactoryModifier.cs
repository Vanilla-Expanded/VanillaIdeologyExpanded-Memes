using System;
using RimWorld;
using Verse;
using UnityEngine;


namespace VanillaMemesExpanded
{
    public class GameComponent_FactoryModifier : GameComponent
    {

        public int tickCounter = 0;
        public int tickInterval = 20000;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterFactories", 0, true);
         
        }


        public GameComponent_FactoryModifier(Game game) : base()
        {

        }

       

        public override void GameComponentTick()
        {


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
