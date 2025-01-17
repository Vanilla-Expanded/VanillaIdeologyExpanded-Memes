﻿using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_HasBeenASlave : ThoughtWorker_Precept_Social
	{
		protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
		{
            if (WorldComponent_AlcoholScarsAndSlaveryTracker.Instance.enslavedPawns.Contains(otherPawn))
            {
				return true;
            }

			else return false;


		}


	}
}
