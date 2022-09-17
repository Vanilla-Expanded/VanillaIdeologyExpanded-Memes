using System;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
	public class Thought_Illness_Exalted : ThoughtWorker_Precept
	{
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			return (p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Illness_Exalted)==true && p.health.hediffSet.AnyHediffMakesSickThought);
		}
	}
}
