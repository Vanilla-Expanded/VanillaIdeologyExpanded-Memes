﻿using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Power_Exalted : ThoughtWorker_Precept
	{

		


		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
            Precept_Role precept_role;
            Precept_Role precept_role3;
            Precept_Role precept_role4;
            return p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Power_Exalted)==true &&

                
                ((precept_role = p.ideo?.Ideo?.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role) != null && precept_role.ChosenPawnSingle() == p) ||
                ((precept_role3 = p.ideo?.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_LeaderSecond) as Precept_Role) != null && precept_role3.ChosenPawnSingle() == p) ||
                ((precept_role4 = p.ideo?.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_LeaderThird) as Precept_Role) != null && precept_role4.ChosenPawnSingle() == p);



		}

	}
}

