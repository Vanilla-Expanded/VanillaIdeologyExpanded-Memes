using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{


	public class RitualRole_BestMelee : RitualRole
	{
		public override bool AppliesToPawn(Pawn p, out string reason, LordJob_Ritual ritual = null, RitualRoleAssignments assignments = null, Precept_Ritual precept = null, bool skipReason = false)
		{
			reason = null;

			Pawn mostSkilledPawn = PawnCollectionClass.mostSkilledPawn;
			
			if (p == mostSkilledPawn)
			{
				return true;
			}
			else
			{
				reason = "VME_PawnMustBeBestMelee".Translate(p);
				return false;
			}
		}

		public override bool AppliesToRole(Precept_Role role, out string reason, Precept_Ritual ritual = null, Pawn pawn = null, bool skipReason = false)
		{
			reason = null;
			return false;
		}



	}
}
