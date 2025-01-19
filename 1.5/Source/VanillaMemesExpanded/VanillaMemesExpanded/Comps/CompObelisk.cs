using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompObelisk : ThingComp
	{
		public CompProperties_Obelisk Props
		{
			get
			{
				return (CompProperties_Obelisk)this.props;
			}
		}


        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            MapComponent_ObeliskTracker mapComp = this.parent.Map.GetComponent<MapComponent_ObeliskTracker>();
            if (mapComp != null)
            {
                mapComp.AddObeliskToMap(this.parent);
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            MapComponent_ObeliskTracker mapComp = this.parent.Map.GetComponent<MapComponent_ObeliskTracker>();
            if (mapComp != null)
            {
                mapComp.AddObeliskToMap(this.parent);
            }
        }
        public override void PostDeSpawn(Map map)
        {
            MapComponent_ObeliskTracker mapComp = this.parent.Map.GetComponent<MapComponent_ObeliskTracker>();
            if (mapComp != null)
            {
                mapComp.RemoveObeliskFromMap(this.parent);
            }
        }

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {

            MapComponent_ObeliskTracker mapComp = this.parent.Map.GetComponent<MapComponent_ObeliskTracker>();
            if (mapComp != null)
            {
                mapComp.RemoveObeliskFromMap(this.parent);
            }
        }

    }
}
