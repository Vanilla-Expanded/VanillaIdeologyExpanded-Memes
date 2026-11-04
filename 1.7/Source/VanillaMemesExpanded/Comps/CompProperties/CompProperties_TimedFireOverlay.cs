using System;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompProperties_TimedFireOverlay : CompProperties_FireOverlay
	{
		public CompProperties_TimedFireOverlay()
		{
			this.compClass = typeof(CompTimedFireOverlay);
		}

		public int minRitualProgress;
		public int upOffset = 0;
	}
}