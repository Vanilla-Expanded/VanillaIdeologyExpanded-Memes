using System;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompProperties_PermanentFireOverlay : CompProperties_FireOverlay
	{
		public CompProperties_PermanentFireOverlay()
		{
			this.compClass = typeof(CompPermanentFireOverlay);
		}

		public int upOffset = 0;
	}
}