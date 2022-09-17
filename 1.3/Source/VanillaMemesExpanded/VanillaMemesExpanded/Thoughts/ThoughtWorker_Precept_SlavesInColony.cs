using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_SlavesInColony : ThoughtWorker_Precept
	{
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			
			return FactionUtility.GetSlavesInFactionCount(p.Faction) > 0;
		}
	}
}
