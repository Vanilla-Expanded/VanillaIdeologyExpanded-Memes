using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;

namespace VanillaMemesExpanded
{
	public class RitualBehaviorWorker_WickerManBurning : RitualBehaviorWorker
	{
		public RitualBehaviorWorker_WickerManBurning()
		{
		}

		public RitualBehaviorWorker_WickerManBurning(RitualBehaviorDef def) : base(def)
		{
		}

		public override string CanStartRitualNow(TargetInfo target, Precept_Ritual ritual, Pawn selectedPawn = null, Dictionary<string, Pawn> forcedForRole = null)
		{
			Precept_Role precept_Role = ritual.ideo.RolesListForReading.FirstOrDefault((Precept_Role r) => r.def == InternalDefOf.VME_IdeoRole_FireKeeper);
			if(precept_Role != null)
            {
				if (precept_Role.ChosenPawnSingle() == null)
				{
					return "CantStartRitualRoleNotAssigned".Translate(precept_Role.LabelCap);
				}
				bool flag = false;
				List<Pawn> listOfPawns = target.Map.mapPawns.FreeColonistsAndPrisonersSpawned;
				foreach (Pawn pawn in listOfPawns)
				{
					if (ValidateConvertee(pawn, precept_Role.ChosenPawnSingle(), false))
					{
						flag = true;
						break;
					}
				}


				if (!flag)
				{
					return "VME_CantStartWickerManRitual".Translate(precept_Role.ChosenPawnSingle().Ideo.name);
				}
			}
			
			return base.CanStartRitualNow(target, ritual, selectedPawn, forcedForRole);
		}

		public static bool ValidateConvertee(Pawn convertee, Pawn leader, bool throwMessages)
		{
			return convertee != null && AbilityUtility.ValidateNoMentalState(convertee, throwMessages) && AbilityUtility.ValidateCanWalk(convertee, throwMessages) && AbilityUtility.ValidateNotSameIdeo(leader, convertee, throwMessages);
		}

		public override void PostCleanup(LordJob_Ritual ritual)
		{
			Pawn inquisitor = ritual.PawnWithRole("inquisitor");
			Pawn prisoner = ritual.PawnWithRole("prisoner");
			if (prisoner.IsPrisonerOfColony)
			{
				Job job = WorkGiver_Warden_TakeToBed.TryMakeJob(inquisitor, prisoner, true);
				if (job != null)
				{
					inquisitor.jobs.StartJob(job, JobCondition.InterruptForced, null, false, true, null, null, false, false);
				}
				
			}
		}
	}
}
