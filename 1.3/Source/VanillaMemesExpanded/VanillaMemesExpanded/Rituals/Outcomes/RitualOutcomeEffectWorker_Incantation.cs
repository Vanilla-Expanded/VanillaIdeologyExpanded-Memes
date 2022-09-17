using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using Verse.Sound;


namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_Incantation : RitualOutcomeEffectWorker_FromQuality
	{
		public RitualOutcomeEffectWorker_Incantation()
		{
		}

		public RitualOutcomeEffectWorker_Incantation(RitualOutcomeEffectDef def) : base(def)
		{
		}

		public override bool GivesDevelopmentPoints => false;

		public override bool SupportsAttachableOutcomeEffect
		{
			get
			{
				return false;
			}
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

				base.GiveMemoryToPawn(pawn, outcome.memory, jobRitual);
				if (outcome.Positive)
				{

					if (pawn.needs != null)
					{
						Need_Corruption need = pawn.needs.TryGetNeed<Need_Corruption>();
						need.CorruptionTaken(0.1f);
						need.CurLevel += 0.1f;
                        if (need.CurLevel > 0.99f)
                        {
							MentalBreakDef mentalBreak = (from x in DefDatabase<MentalBreakDef>.AllDefsListForReading
														 where x.Worker.BreakCanOccur(pawn) && x.intensity== MentalBreakIntensity.Extreme
														  select x).RandomElement();
							TaggedString t = "VME_BreakCorruption".Translate();
							mentalBreak.Worker.TryStart(pawn,t,false);

						}


					}

				}
			}
			

			



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




		}


	}
}
