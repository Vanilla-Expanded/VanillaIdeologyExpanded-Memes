using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Alcohol_MildAbstinence : ThoughtWorker_Precept
	{

		public int period = 180000; //3 days
		



		protected override ThoughtState ShouldHaveThought(Pawn p)
		{

			if (PawnCollectionClass.colonist_booze_tracker.ContainsKey(p))
			{
				if (PawnCollectionClass.colonist_booze_tracker[p] < period)
				{
					return ThoughtState.ActiveAtStage(0);

				}
				else 
				{
					return ThoughtState.ActiveAtStage(1);

				}
				
			}
			else return false;








		}


	}
}
