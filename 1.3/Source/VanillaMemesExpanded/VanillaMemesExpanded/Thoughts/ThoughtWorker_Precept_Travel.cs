using System;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Travel : ThoughtWorker_Precept
	{

		public int firstPeriod = 300000; //5 days
		public int secondPeriod = 600000; //10 days
		public int thirdPeriod = 900000; //15 days
		public int maxPeriod = 3600000; //60 days

		
		
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{

			if (PawnCollectionClass.colonist_caravan_tracker.ContainsKey(p)) {
				if (PawnCollectionClass.colonist_caravan_tracker[p] < firstPeriod)
				{
					return ThoughtState.ActiveAtStage(0);

				}
				else if (PawnCollectionClass.colonist_caravan_tracker[p] < secondPeriod)
				{
					return ThoughtState.ActiveAtStage(1);

				}
				else if (PawnCollectionClass.colonist_caravan_tracker[p] < thirdPeriod)
				{
					return ThoughtState.ActiveAtStage(2);

				}
				else if (PawnCollectionClass.colonist_caravan_tracker[p] < maxPeriod)
				{
					return ThoughtState.ActiveAtStage(3);

				}

				else
				{
					return false;
				}
			} else return false;

			






		}


	}
}
