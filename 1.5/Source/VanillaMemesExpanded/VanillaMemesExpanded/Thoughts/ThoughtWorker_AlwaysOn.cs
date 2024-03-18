using System;
using Verse;
using RimWorld;




namespace VanillaMemesExpanded
{
	public class ThoughtWorker_AlwaysOn : ThoughtWorker_Precept
	{
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			
			return true;
		}
	}
}

