using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Leader_Godlike : ThoughtWorker_Precept_Social
	{
		protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
		{
			Precept_Role precept_role;
            Precept_Role precept_role3;
            Precept_Role precept_role4;

            if (p.Faction == otherPawn.Faction && p.ideo?.Ideo == otherPawn.ideo?.Ideo && p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Leader_Godlike)==true &&
				(
					(precept_role = p.ideo?.Ideo?.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role) != null && precept_role.ChosenPawnSingle() == otherPawn
					||
					((precept_role3 = p.ideo?.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_LeaderSecond) as Precept_Role) != null && precept_role3.ChosenPawnSingle() == otherPawn) ||
					((precept_role4 = p.ideo?.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_LeaderThird) as Precept_Role) != null && precept_role4.ChosenPawnSingle() == otherPawn)
				
				)
			)
            {
				if (p.ideo.Certainty < 0.25f)
				{
					return ThoughtState.ActiveAtStage(3);
				}
				else if (p.ideo.Certainty < 0.50f)
				{
					return ThoughtState.ActiveAtStage(2);
				}
				else if (p.ideo.Certainty < 0.75f)
				{
					return ThoughtState.ActiveAtStage(1);
				}
				else
				{
					return ThoughtState.ActiveAtStage(0);
				}


			}
			else return false;

			


		}

       
    }
}
