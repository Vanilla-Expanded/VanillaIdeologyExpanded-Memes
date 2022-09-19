using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class JobDriver_DeconstructBuilding : JobDriver
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
					CompDeconstructJunk comp = building.TryGetComp<CompDeconstructJunk>();
					if (comp != null)
                    {
						ThingDef buildingToPopUp = comp.Props.defToPopUpMinified;
						Thing thing = ThingMaker.MakeThing(buildingToPopUp, null);
						thing = MinifyUtility.MakeMinified(thing);
						GenPlace.TryPlaceThing(thing, building.Position, Find.CurrentMap, ThingPlaceMode.Direct, null, null, default(Rot4));

					}
					building.Destroy(DestroyMode.Vanish);
				},
				defaultCompleteMode = ToilCompleteMode.Instant
			};
			yield break;
		}
	}
}
