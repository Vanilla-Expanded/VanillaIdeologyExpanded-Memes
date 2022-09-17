using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompProperties_AbilityHarvestBodyParts : CompProperties_AbilityEffect
	{
		public CompProperties_AbilityHarvestBodyParts()
		{
			this.compClass = typeof(CompAbilityHarvestBodyParts);
		}

		public List<ThingDef> bodyParts;
		
	}
}
