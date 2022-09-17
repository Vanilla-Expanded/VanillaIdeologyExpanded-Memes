using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Alcohol : ThoughtWorker_Precept
	{

		public int firstPeriod = 180000; //3 days
		public int secondPeriod = 600000; //5 days
		public int thirdPeriod = 900000; //10 days
		public int fourthPeriod = 900000; //20 days
		public int maxPeriod = 1800000; //30 days


		

		protected override ThoughtState ShouldHaveThought(Pawn p)
		{

			if (PawnCollectionClass.colonist_booze_tracker.ContainsKey(p)) {
				if (PawnCollectionClass.colonist_booze_tracker[p] < firstPeriod)
				{
					return false;

				}
				else if (PawnCollectionClass.colonist_booze_tracker[p] < secondPeriod)
				{
					return ThoughtState.ActiveAtStage(0);

				}
				else if (PawnCollectionClass.colonist_booze_tracker[p] < thirdPeriod)
				{
					return ThoughtState.ActiveAtStage(1);

				}
				else if (PawnCollectionClass.colonist_booze_tracker[p] < fourthPeriod)
				{
					return ThoughtState.ActiveAtStage(2);

				}
				else if (PawnCollectionClass.colonist_booze_tracker[p] < maxPeriod)
				{
					return ThoughtState.ActiveAtStage(3);

				}

				else
				{
					return ThoughtState.ActiveAtStage(4);
				}
			} else return false;

			






		}


	}
}
