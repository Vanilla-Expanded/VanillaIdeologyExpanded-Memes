using System;
using Verse;
using System.Collections.Generic;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_SharedMood : ThoughtWorker_Precept
	{

		

		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (PawnCollectionClass.averageColonyMood == 0)
			{
				return false;

			}
			if (PawnCollectionClass.averageColonyMood>0)
			{
				return ThoughtState.ActiveAtStage(0);

			}
			else return ThoughtState.ActiveAtStage(1);






		}

	}
}
