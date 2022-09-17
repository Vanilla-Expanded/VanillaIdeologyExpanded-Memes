// RimWorld.RitualOutcomeEffectWorker_Conversion
using System.Collections.Generic;
using RimWorld;
using Verse;



namespace VanillaMemesExpanded { 
public class RitualOutcomeEffectWorker_LeaderConversion : RitualOutcomeEffectWorker_FromQuality
{
	public override bool SupportsAttachableOutcomeEffect => false;

	public RitualOutcomeEffectWorker_LeaderConversion()
	{
	}

	public RitualOutcomeEffectWorker_LeaderConversion(RitualOutcomeEffectDef def)
		: base(def)
	{
	}

	public override void Apply(float progress, Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual)
	{
		float quality = GetQuality(jobRitual, progress);
		OutcomeChance outcome = GetOutcome(quality, jobRitual);
		LookTargets letterLookTargets = jobRitual.selectedTarget;
		string extraLetterText = null;
		if (jobRitual.Ritual != null)
		{
			ApplyAttachableOutcome(totalPresence, jobRitual, outcome, out extraLetterText, ref letterLookTargets);
		}
		Pawn pawn = jobRitual.PawnWithRole("leader");
		Pawn pawn2 = jobRitual.PawnWithRole("convertee");
		float ideoCertaintyOffset = outcome.ideoCertaintyOffset;
		if (ideoCertaintyOffset <= -1f)
		{
			pawn2.ideo.SetIdeo(pawn.Ideo);
		}
		else
		{
			pawn2.ideo.OffsetCertainty(ideoCertaintyOffset);
		}
		foreach (Pawn key in totalPresence.Keys)
		{
			if (key != pawn && key != pawn2 && outcome.memory != null)
			{
				Thought_AttendedRitual newThought = (Thought_AttendedRitual)MakeMemory(key, jobRitual, outcome.memory);
				key.needs.mood.thoughts.memories.TryGainMemory(newThought);
			}
		}
		TaggedString text = outcome.description.Formatted(jobRitual.Ritual.Label).CapitalizeFirst();
		string text2 = def.OutcomeMoodBreakdown(outcome);
		if (!text2.NullOrEmpty())
		{
			text += "\n\n" + text2;
		}
		if (extraLetterText != null)
		{
			text += "\n\n" + extraLetterText;
		}
		text += "\n\n" + OutcomeQualityBreakdownDesc(quality, progress, jobRitual);
		ApplyDevelopmentPoints(jobRitual.Ritual, outcome, out var extraOutcomeDesc);
		if (extraOutcomeDesc != null)
		{
			text += "\n\n" + extraOutcomeDesc;
		}
		Find.LetterStack.ReceiveLetter("OutcomeLetterLabel".Translate(outcome.label.Named("OUTCOMELABEL"), jobRitual.Ritual.Label.Named("RITUALLABEL")), text, outcome.Positive ? LetterDefOf.RitualOutcomePositive : LetterDefOf.RitualOutcomeNegative, letterLookTargets);
	}
}
}
