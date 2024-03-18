using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompAbilityMedicalEmergency : CompAbilityEffect
	{
		public new CompProperties_AbilityMedicalEmergency Props
		{
			get
			{
				return (CompProperties_AbilityMedicalEmergency)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{

			Pawn pawn = this.parent.pawn;
			pawn.health.AddHediff(InternalDefOf.VME_MedicalEmergencyHediff);

			base.Apply(target, dest);

		}
	}
}
