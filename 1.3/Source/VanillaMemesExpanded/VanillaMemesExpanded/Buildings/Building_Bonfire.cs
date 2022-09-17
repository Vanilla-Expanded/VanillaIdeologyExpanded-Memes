using System;
using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{

    public class Building_Bonfire : Building
    {

        public int timeToDie = 100;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.timeToDie, "timeToDie", 0, true);

        }

        public override void Tick()
        {
            if (this.IsHashIntervalTick(18000))
            {
                if (timeToDie > 0) { timeToDie--; } else { this.Destroy(); }

            }



            base.Tick();
        }

        public override string GetInspectString()
        {
            

            return "VME_BonfireDyingIn".Translate(timeToDie);
        }
    }
}
