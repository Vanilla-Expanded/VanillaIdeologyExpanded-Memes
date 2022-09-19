using System;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
	public class Thought_Illness_Exalted_TooHealthy : ThoughtWorker_Precept
	{
		public const int tickInterval = 300000; //5 days


		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Illness_Exalted)!=true)
            {
				return false;
			}

            if (p.health.hediffSet.AnyHediffMakesSickThought)
            {
				
				return false;
            }


			if (PawnCollectionClass.colonist_illness_tracker.ContainsKey(p)&&PawnCollectionClass.colonist_illness_tracker[p] > tickInterval)
			{
				return true;
			}
			return false;

		}
	}
}