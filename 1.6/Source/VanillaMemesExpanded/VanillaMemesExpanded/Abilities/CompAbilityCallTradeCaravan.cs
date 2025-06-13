using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompAbilityCallTradeCaravan : CompAbilityEffect
	{
		public new CompProperties_AbilityCallTradeCaravan Props
		{
			get
			{
				return (CompProperties_AbilityCallTradeCaravan)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{

			MapComponent_TradeCaravanDelay comp = this.parent.pawn.Map.GetComponent<MapComponent_TradeCaravanDelay>();
			if (comp != null)
            {
				comp.signaledCaravanArriving = true;
				Messages.Message("VME_AbilityCaravanWillArrive".Translate(), MessageTypeDefOf.CautionInput, true);
			}


			base.Apply(target, dest);

		}
	}
}
