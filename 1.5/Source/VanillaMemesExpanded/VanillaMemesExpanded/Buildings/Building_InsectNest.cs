using System;
using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{

    public class Building_InsectNest : Building
    {

        public int maintenance = 100;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.maintenance, "maintenance", 0, true);

        }

        public override void Tick()
        {
            if (this.IsHashIntervalTick(2400))
            {
                if (maintenance > 0) { maintenance--; } else { this.Destroy(); }



            }



            base.Tick();
        }

        public override string GetInspectString()
        {
            string maintenanceNeededString = "";
            if (maintenance < 50)
            {
                maintenanceNeededString = "\n" + "VME_MaintenanceNeeded".Translate();
            }

            return base.GetInspectString() + "\n" + "VME_Maintenance".Translate(maintenance) + maintenanceNeededString;
        }
    }
}
