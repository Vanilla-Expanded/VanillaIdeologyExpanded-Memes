using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Hospital : ThoughtWorker_Precept
	{


		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (p.Map?.IsPlayerHome != true)
			{
				return false;
			}
			
			if (PawnCollectionClass.hospitalTilesInMap ==0)
			{
				return ThoughtState.ActiveAtStage(0);
			}else if (PawnCollectionClass.hospitalDirty)
			{
				return ThoughtState.ActiveAtStage(1);
			}
			else if (PawnCollectionClass.hospitalTilesInMap < 25)
			{
				return ThoughtState.ActiveAtStage(2);
			}
			else if (PawnCollectionClass.hospitalTilesInMap < 50)
			{
				return ThoughtState.ActiveAtStage(3);
			}
			
			else
			{
				return ThoughtState.ActiveAtStage(4);
			}





		}


	}
}
