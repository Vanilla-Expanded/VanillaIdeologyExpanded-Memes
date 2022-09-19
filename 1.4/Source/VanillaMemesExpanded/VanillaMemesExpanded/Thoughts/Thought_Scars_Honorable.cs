using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class Thought_Scars_Honorable : ThoughtWorker_Precept_Social
	{
		protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
		{
			
			if (!PawnCollectionClass.colonist_scar_counter.ContainsKey(otherPawn))
            {
				return false;
            } else if (PawnCollectionClass.colonist_scar_counter[otherPawn]==0) {
				return false;

			}
            else
            {
                switch (PawnCollectionClass.colonist_scar_counter[otherPawn])
                {
					case 1:
						return ThoughtState.ActiveAtStage(0);
						
					case 2:
						return ThoughtState.ActiveAtStage(1);
						
					case 3:
						return ThoughtState.ActiveAtStage(2);
					case 4:
						return ThoughtState.ActiveAtStage(3);
					
					default:
						return ThoughtState.ActiveAtStage(4);
				}


            } 



		}


	}
}
