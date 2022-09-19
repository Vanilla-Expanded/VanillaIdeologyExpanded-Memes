using System;
using Verse;
using System.Collections.Generic;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Trading : ThoughtWorker_Precept
	{

		public const int maxTimeWithoutTrading = 420000; //7 days

		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (PawnCollectionClass.ticksWithoutTrading> maxTimeWithoutTrading)
            {
				return ThoughtState.ActiveAtStage(1);

			} else return ThoughtState.ActiveAtStage(0);






		}

	}
}
