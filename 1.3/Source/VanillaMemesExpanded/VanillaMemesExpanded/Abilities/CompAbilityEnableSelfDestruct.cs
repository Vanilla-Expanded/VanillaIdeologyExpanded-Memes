using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompAbilityEnableSelfDestruct : CompAbilityEffect
	{
		public new CompProperties_AbilityEnableSelfDestruct Props
		{
			get
			{
				return (CompProperties_AbilityEnableSelfDestruct)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			Building_TurretGun turret = target.Thing as Building_TurretGun;
			Pawn mechanoid = target.Thing as Pawn;

			if (turret != null) {
				CompExplosive comp = turret.TryGetComp<CompExplosive>();
                if (comp != null)
                {
					comp.StartWick(this.parent.pawn);
                }
                else
                {
					GenExplosion.DoExplosion(target.Cell, target.Thing.Map, 3.9f, DamageDefOf.Bomb, this.parent.pawn, -1, -1f, null, null, null, null, null, 0f, 1, false, null, 0f, 1, 0f, false, null, null);
					turret.Destroy();
				}


			} else if( mechanoid != null)
			{

                if (mechanoid.RaceProps.IsMechanoid) {
					GenExplosion.DoExplosion(target.Cell, target.Thing.Map, 5.9f, DamageDefOf.Bomb, this.parent.pawn, -1, -1f, null, null, null, null, null, 0f, 1, false, null, 0f, 1, 0f, false, null, null);
					mechanoid.Kill(null);

                }
                else {
					Messages.Message("VME_AbilityNeedsATurret".Translate(), MessageTypeDefOf.RejectInput, true);
					this.parent.StartCooldown(30);
				}
				


			}
            else{
				Messages.Message("VME_AbilityNeedsATurret".Translate(), MessageTypeDefOf.RejectInput, true);
				this.parent.StartCooldown(30);
			}



			base.Apply(target, dest);

		}
	}
}
