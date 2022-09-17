using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class MapComponent_TradeCaravanDelay : MapComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 60000;
        public bool signaledCaravanArriving = false;
     


        public MapComponent_TradeCaravanDelay(Map map) : base(map)
        {

        }

      

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterCaravan", 0, true);
            Scribe_Values.Look<bool>(ref this.signaledCaravanArriving, "signaledCaravanArriving", false, true);


        }
        public override void MapComponentTick()
        {

            if (signaledCaravanArriving)
            {
                tickCounter++;
                if ((tickCounter > tickInterval))
                {

                    IncidentDef IncidentDef = IncidentDefOf.TraderCaravanArrival;
                    IncidentParms parms = StorytellerUtility.DefaultParmsNow(IncidentDef.category, map);
                    IncidentDef.Worker.TryExecute(parms);

                    signaledCaravanArriving = false;
                    tickCounter = 0;
                }

            }


            



        }
       



    }


}



