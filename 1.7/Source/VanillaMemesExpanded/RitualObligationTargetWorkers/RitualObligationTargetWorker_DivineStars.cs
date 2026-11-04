using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class RitualObligationTargetWorker_DivineStars : RitualObligationTargetWorker_ThingDef
	{
		public RitualObligationTargetWorker_DivineStars()
		{
		}

		public RitualObligationTargetWorker_DivineStars(RitualObligationTargetFilterDef def) : base(def)
		{
		}

		public override IEnumerable<TargetInfo> GetTargets(RitualObligation obligation, Map map)
		{
			if (!ModLister.CheckIdeology("Skylantern target"))
			{
				yield break;
			}
			List<Thing> ritualSpot = map.listerThings.ThingsOfDef(ThingDefOf.RitualSpot);
			int num;
			for (int i = 0; i < ritualSpot.Count; i = num + 1)
			{
				yield return ritualSpot[i];
				num = i;
			}
			
			yield break;
		}

		protected override RitualTargetUseReport CanUseTargetInternal(TargetInfo target, RitualObligation obligation)
		{
			Thing thing = target.Thing;
			if (thing.def != ThingDefOf.RitualSpot )
			{
				return false;
			}
			Room room = thing.GetRoom(RegionType.Set_All);
			if (this.def.colonistThingsOnly && (thing.Faction == null || !thing.Faction.IsPlayer))
			{
				return false;
			}
            if (GenCelestial.CurCelestialSunGlow(thing.Map) > 0.5f) {
				return "VME_RitualNeedsNightOrDarkness".Translate();
			}

			if (thing.Map.weatherManager.curWeather == WeatherDef.Named("Fog")|| thing.Map.weatherManager.curWeather.rainRate>0)
			{
				return "VME_RitualNeedsClearWeather".Translate();
			}


			int num = 0;
			foreach (IntVec3 intVec in GenRadial.RadialCellsAround(target.Cell, (float)this.def.unroofedCellSearchRadius, false))
			{
				if (intVec.InBounds(target.Map) && !intVec.Roofed(target.Map) && intVec.GetRoom(thing.Map) == room)
				{
					num++;
					if (num >= this.def.minUnroofedCells)
					{
						break;
					}
				}
			}
			if (num < this.def.minUnroofedCells)
			{
				return "RitualTargetNeedUnroofedCells".Translate(this.def.minUnroofedCells);
			}
			if (thing.def == ThingDefOf.RitualSpot)
			{
				return true;
			}

			
			return false;
		}

		public override IEnumerable<string> GetTargetInfos(RitualObligation obligation)
		{
			yield return "VME_RitualTargetDivineTheStarsInfo".Translate();
			yield break;
		}

		
	}
}
