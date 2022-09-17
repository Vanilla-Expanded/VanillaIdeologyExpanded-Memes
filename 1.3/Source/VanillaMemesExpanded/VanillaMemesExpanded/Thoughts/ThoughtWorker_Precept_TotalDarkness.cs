using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_TotalDarkness : ThoughtWorker_Precept
	{
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (!p.Awake() || PawnUtility.IsBiologicallyOrArtificallyBlind(p))
			{
				return false;
			}
			return p.Map.glowGrid.PsychGlowAt(p.Position) == PsychGlow.Dark;
		}
	}
}
