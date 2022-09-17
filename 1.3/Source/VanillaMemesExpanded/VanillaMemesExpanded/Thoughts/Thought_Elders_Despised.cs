using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class Thought_Elders_Despised : ThoughtWorker_Precept_Social
	{
		protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
		{
			if (otherPawn.ageTracker.AgeBiologicalYears >= 65)
			{
				return ThoughtState.ActiveAtStage(0);
			}

			if (otherPawn.ageTracker.AgeBiologicalYears <= 25)
			{
				return ThoughtState.ActiveAtStage(1);
			}

			else return false;


		}


	}
}
