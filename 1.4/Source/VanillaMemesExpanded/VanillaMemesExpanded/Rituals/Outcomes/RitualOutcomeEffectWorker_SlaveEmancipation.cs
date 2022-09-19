using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using System.Linq;

namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_SlaveEmancipation : RitualOutcomeEffectWorker_FromQuality
	{
		public override bool SupportsAttachableOutcomeEffect
		{
			get
			{
				return false;
			}
		}

		public override bool GivesDevelopmentPoints => false;

		public RitualOutcomeEffectWorker_SlaveEmancipation()
		{
		}

		public RitualOutcomeEffectWorker_SlaveEmancipation(RitualOutcomeEffectDef def) : base(def)
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
			List<Pawn> slaves = new List<Pawn>();
			foreach (Pawn pawn in totalPresence.Keys)
			{
                if (pawn.IsSlaveOfColony)
                {
					slaves.Add(pawn);
                }
				base.GiveMemoryToPawn(pawn, outcome.memory, jobRitual);

			}
			float percentage = 0;
            switch (outcome.positivityIndex)
            {
				
				case 1:
					percentage = 0.25f;
					break;
				case 2:
					percentage = 0.5f;
					break;
				case 3:
					percentage = 0.75f;
					break;
				case 4:
					percentage = 1f;
					break;
				default:
					break;

			}
			Pawn recruiter = jobRitual.PawnWithRole("leader");
			Random random = new Random();
			foreach (Pawn pawn in slaves)
			{
				GenGuest.SlaveRelease(pawn);
				if (random.NextDouble()< percentage)
                {
					InteractionWorker_RecruitAttempt.DoRecruit(recruiter, pawn);

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
