using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_WickerManBurning : RitualOutcomeEffectWorker_FromQuality
	{
		public override bool SupportsAttachableOutcomeEffect
		{
			get
			{
				return false;
			}
		}

		public RitualOutcomeEffectWorker_WickerManBurning()
		{
		}

		public RitualOutcomeEffectWorker_WickerManBurning(RitualOutcomeEffectDef def) : base(def)
		{
		}

		public override void Apply(float progress, Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual)
		{
			float quality = base.GetQuality(jobRitual, progress);
			OutcomeChance outcome = this.GetOutcome(quality, jobRitual);
			LookTargets lookTargets = jobRitual.selectedTarget;
			string text = null;
			if (jobRitual.Ritual != null)
			{
				this.ApplyAttachableOutcome(totalPresence, jobRitual, outcome, out text, ref lookTargets);
			}
			bool flag = false;
			foreach (Pawn pawn in totalPresence.Keys)
			{
				if (pawn.IsSlave)
				{
					Need_Suppression need_Suppression = pawn.needs.TryGetNeed<Need_Suppression>();
					if (need_Suppression != null)
					{
						need_Suppression.CurLevel = 1f;
					}
					flag = true;
				}
				else
				{
					base.GiveMemoryToPawn(pawn, outcome.memory, jobRitual);
				}

			}
			


			Pawn prisoner = jobRitual.PawnWithRole("prisoner");
			Pawn inquisitor = jobRitual.PawnWithRole("inquisitor");
			prisoner.guilt.Notify_Guilty(900000);
			prisoner.health.killedByRitual = true;
			do
			{
				BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(prisoner, RulePackDefOf.DamageEvent_Fire, null);
				Find.BattleLog.Add(battleLogEntry_DamageTaken);
				DamageInfo dinfo = new DamageInfo(DamageDefOf.Flame, (float)10, 0f, -1f, inquisitor, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true);
				dinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
				prisoner.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_DamageTaken);
				Apparel apparel;
				if (prisoner.apparel != null && prisoner.apparel.WornApparel.TryRandomElement(out apparel))
				{
					apparel.TakeDamage(new DamageInfo(DamageDefOf.Flame, (float)10, 0f, -1f, inquisitor, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true));
					
				}

			} while (!prisoner.Dead);








			string text2 = outcome.description.Formatted(jobRitual.Ritual.Label).CapitalizeFirst() + "\n\n" + this.OutcomeQualityBreakdownDesc(quality, progress, jobRitual);
			string text3 = this.def.OutcomeMoodBreakdown(outcome);
			if (!text3.NullOrEmpty())
			{
				text2 = text2 + "\n\n" + text3;
			}
			if (flag)
			{
				text2 += "\n\n" + "RitualOutcomeExtraDesc_Execution".Translate();
			}
			if (text != null)
			{
				text2 = text2 + "\n\n" + text;
			}
			string text4;
			this.ApplyDevelopmentPoints(jobRitual.Ritual, outcome, out text4);
			if (text4 != null)
			{
				text2 = text2 + "\n\n" + text4;
			}
			Find.LetterStack.ReceiveLetter("OutcomeLetterLabel".Translate(outcome.label.Named("OUTCOMELABEL"), jobRitual.Ritual.Label.Named("RITUALLABEL")), text2, outcome.Positive ? LetterDefOf.RitualOutcomePositive : LetterDefOf.RitualOutcomeNegative, lookTargets, null, null, null, null);
			Building altar = jobRitual.selectedTarget.Thing as Building;
			altar.Destroy();
		
		}
	}
}
