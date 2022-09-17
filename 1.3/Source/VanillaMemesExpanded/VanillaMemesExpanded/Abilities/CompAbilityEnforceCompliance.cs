using System;
using Verse;
using RimWorld;
using Verse.Sound;

namespace VanillaMemesExpanded
{
	public class CompAbilityEnforceCompliance : CompAbilityEffect
	{
		public new CompProperties_AbilityEnforceCompliance Props
		{
			get
			{
				return (CompProperties_AbilityEnforceCompliance)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			Pawn victim = target.Thing as Pawn;

			if (victim != null && victim != this.parent.pawn && victim.Faction?.IsPlayer==true)
			{

				if (victim.IsSlave||victim.IsPrisoner)
				{
					Messages.Message("VME_AbilityNeedsAColonist".Translate(), MessageTypeDefOf.RejectInput, true);
					this.parent.StartCooldown(30);

                }
                else {
					victim.guilt.Notify_Guilty(900000);
					victim.health.killedByRitual = true;
					ExecutionUtility.DoExecutionByCut(this.parent.pawn, victim);
					
					foreach (Pawn pawn in PawnsFinder.AllMaps_SpawnedPawnsInFaction(Faction.OfPlayer))
					{
						if (pawn.RaceProps.Humanlike && (pawn.Ideo == this.parent.pawn.Ideo))
						{
							pawn.needs.mood.thoughts.memories.TryGainMemory(InternalDefOf.VME_EnforcedCompliance, null, this.parent.pawn.Ideo.GetPrecept(InternalDefOf.VME_IdeoRole_Commissar));
						}
					}
				}
					


			}
			
			else
			{
				Messages.Message("VME_AbilityNeedsAColonist".Translate(), MessageTypeDefOf.RejectInput, true);
				this.parent.StartCooldown(30);
			}



			base.Apply(target, dest);

		}
	}
}
