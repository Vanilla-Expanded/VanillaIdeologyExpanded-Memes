using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompProperties_AbilityMakeADessert : CompProperties_AbilityEffect
	{
		public CompProperties_AbilityMakeADessert()
		{
			this.compClass = typeof(CompAbilityMakeADessert);
		}

		public ThingDef simpleDessertDef;
		public ThingDef fineDessertDef;
		public ThingDef lavishDessertDef;
		public ThingDef gourmetDessertDef;

	}
}
