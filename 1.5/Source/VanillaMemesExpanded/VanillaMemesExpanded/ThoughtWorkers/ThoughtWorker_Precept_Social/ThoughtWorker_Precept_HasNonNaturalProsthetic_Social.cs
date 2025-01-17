using System;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_HasNonNaturalProsthetic_Social : ThoughtWorker_Precept_Social
	{
		protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
		{
			return CountAddedAndImplantedParts(otherPawn.health.hediffSet) > 0;
		}

		public int CountAddedAndImplantedParts(HediffSet hs)
		{
			int num = 0;
			List<Hediff> hediffs = hs.hediffs;
			for (int i = 0; i < hediffs.Count; i++)
			{
				if (hediffs[i]?.def?.countsAsAddedPartOrImplant == true)
				{
					if (hediffs[i]?.def?.spawnThingOnRemoved?.thingCategories?.Contains(ThingCategoryDef.Named("BodyPartsNatural")) == false &&
						hediffs[i]?.def?.spawnThingOnRemoved?.thingCategories?.Contains(DefDatabase<ThingCategoryDef>.GetNamedSilentFail("AA_ImplantCategory")) == false &&
						hediffs[i]?.def?.spawnThingOnRemoved?.thingCategories?.Contains(DefDatabase<ThingCategoryDef>.GetNamedSilentFail("VFEI_BodyPartsInsect")) == false &&
						hediffs[i]?.def?.spawnThingOnRemoved?.thingCategories?.Contains(DefDatabase<ThingCategoryDef>.GetNamedSilentFail("GR_ImplantCategory")) == false)
					{
						num++;
					}




				}
			}
			return num;
		}
	}
}