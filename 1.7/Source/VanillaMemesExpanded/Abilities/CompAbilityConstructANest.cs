using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompAbilityConstructANest : CompAbilityEffect
	{
		public new CompProperties_AbilityConstructANest Props
		{
			get
			{
				return (CompProperties_AbilityConstructANest)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			IntVec3 loc = target.Cell;
			Map map = this.parent.pawn.Map;

			if (loc.InBounds(map)&&loc.GetRoof(map) == RoofDefOf.RoofRockThick&& loc.GetEdifice(map)==null)
			{

				ThingDef newThing = InternalDefOf.VME_InsectNest;
				Thing hive = GenSpawn.Spawn(newThing, loc, map, WipeMode.Vanish);
				hive.SetFaction(Faction.OfPlayer);


			}
			else
			{
				Messages.Message("VME_AbilityNeedsRoof".Translate(), MessageTypeDefOf.RejectInput, true);
				this.parent.StartCooldown(30);
			}



			base.Apply(target, dest);

		}
	}
}
