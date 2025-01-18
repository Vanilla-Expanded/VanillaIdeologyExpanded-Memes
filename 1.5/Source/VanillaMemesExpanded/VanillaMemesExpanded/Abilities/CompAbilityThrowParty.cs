using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompAbilityThrowParty : CompAbilityEffect
	{
		public new CompProperties_AbilityThrowParty Props
		{
			get
			{
				return (CompProperties_AbilityThrowParty)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			
			if (!parent.pawn.Map.lordsStarter.TryStartRandomGathering(true))
			{
				Messages.Message("VME_NoValidPartySpot".Translate(), MessageTypeDefOf.RejectInput, false);
                this.parent.StartCooldown(30);
            }
			base.Apply(target, dest);

		}
	}
}
