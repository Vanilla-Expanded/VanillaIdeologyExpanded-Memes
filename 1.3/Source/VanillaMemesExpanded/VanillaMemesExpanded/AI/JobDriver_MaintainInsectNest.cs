using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class JobDriver_MaintainInsectNest : JobDriver
	{
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return this.pawn.Reserve(this.job.GetTarget(TargetIndex.A).Thing, this.job, 1, -1, null, true);
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			Thing building = this.job.GetTarget(TargetIndex.A).Thing;
			this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
			this.FailOnBurningImmobile(TargetIndex.A);
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
			yield return Toils_General.Wait(1200, TargetIndex.None).FailOnDestroyedNullOrForbidden(TargetIndex.A).FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch).WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
			yield return new Toil
			{
				initAction = delegate ()
				{
					Building_InsectNest buildingNest = building as Building_InsectNest;
					buildingNest.maintenance = 100;
				},
				defaultCompleteMode = ToilCompleteMode.Instant
			};
			yield break;
		}
	}
}
