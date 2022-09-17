using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_ViolentConversion : RitualOutcomeEffectWorker_FromQuality
	{
		public override bool SupportsAttachableOutcomeEffect
		{
			get
			{
				return false;
			}
		}

		public RitualOutcomeEffectWorker_ViolentConversion()
		{
		}

		public RitualOutcomeEffectWorker_ViolentConversion(RitualOutcomeEffectDef def) : base(def)
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
				
					base.GiveMemoryToPawn(pawn, outcome.memory, jobRitual);
				
			}

			Random random = new Random();

			Pawn prisoner = jobRitual.PawnWithRole("prisoner");
			Pawn inquisitor = jobRitual.PawnWithRole("inquisitor");
			bool violentOutcome = false;
			if (random.NextDouble() > 0.5)
			{
				
				prisoner.ideo.SetIdeo(inquisitor.Ideo);
				

			}
            else {

				prisoner.guilt.Notify_Guilty(900000);
				prisoner.health.killedByRitual = true;
				ExecutionUtility.DoExecutionByCut(inquisitor, prisoner, 8, true);
				ThoughtUtility.GiveThoughtsForPawnExecuted(prisoner, inquisitor, PawnExecutionKind.GenericBrutal);
				TaleRecorder.RecordTale(TaleDefOf.ExecutedPrisoner, new object[]
				{
					inquisitor,
					prisoner
				});
				violentOutcome = true;
			}



				string text2 = outcome.description.Formatted(jobRitual.Ritual.Label).CapitalizeFirst() + "\n\n" + this.OutcomeQualityBreakdownDesc(quality, progress, jobRitual);
			string text3 = this.def.OutcomeMoodBreakdown(outcome);
			string textViolentOutcome="";
            if (violentOutcome)
            {
				textViolentOutcome = "VME_ViolentOutcome".Translate(prisoner.LabelCap);

			}
            else
            {
				textViolentOutcome = "VME_NonViolentOutcome".Translate(prisoner.LabelCap);
			}

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
			if (textViolentOutcome != "")
			{
				text2 = text2 + "\n\n" + textViolentOutcome;
			}
			Find.LetterStack.ReceiveLetter("OutcomeLetterLabel".Translate(outcome.label.Named("OUTCOMELABEL"), jobRitual.Ritual.Label.Named("RITUALLABEL")), text2, outcome.Positive ? LetterDefOf.RitualOutcomePositive : LetterDefOf.RitualOutcomeNegative, lookTargets, null, null, null, null);
		}
	}
}
