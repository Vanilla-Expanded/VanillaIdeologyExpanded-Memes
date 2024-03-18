using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class Thought_Elders_Respected : ThoughtWorker_Precept_Social
	{
		protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
		{
			if (otherPawn.ageTracker.AgeBiologicalYears >= 65)
			{
				return true;
			}
			else return false;


		}


	}
}
