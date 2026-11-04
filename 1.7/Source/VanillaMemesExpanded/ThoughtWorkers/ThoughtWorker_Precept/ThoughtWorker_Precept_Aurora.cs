using System;
using Verse;
using RimWorld;




namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Aurora : ThoughtWorker_Precept
	{
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (!p.Spawned)
			{
				return false;
			}
			if (!p.Awake() || PawnUtility.IsBiologicallyOrArtificiallyBlind(p))
			{
				return false;
			}
			Room room = p.GetRoom(RegionType.Set_All);
			if (room != null && !room.PsychologicallyOutdoors)
			{
				return false;
			}
			return p.Map.GameConditionManager.ConditionIsActive(GameConditionDefOf.Aurora);
		}
	}
}

