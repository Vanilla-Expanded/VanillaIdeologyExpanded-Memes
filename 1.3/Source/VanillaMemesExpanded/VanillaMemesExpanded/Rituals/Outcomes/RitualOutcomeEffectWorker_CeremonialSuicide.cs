using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using Verse.Sound;
using UnityEngine;


namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_CeremonialSuicide : RitualOutcomeEffectWorker_FromQuality
	{
		public RitualOutcomeEffectWorker_CeremonialSuicide()
		{
		}

		public RitualOutcomeEffectWorker_CeremonialSuicide(RitualOutcomeEffectDef def) : base(def)
		{
		}

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
			foreach (Pawn pawn in totalPresence.Keys)
			{

				base.GiveMemoryToPawn(pawn, outcome.memory, jobRitual);

			}
			Pawn suicide = jobRitual.PawnWithRole("suicide");

			RitualObligation ritualObligationToRemove =null;
			foreach (RitualObligation ritualObligation in jobRitual.Ritual.activeObligations)
			{
				Pawn pawn = ritualObligation.targetA.Thing as Pawn;
				if (pawn != null && pawn== suicide)
				{
					ritualObligationToRemove = ritualObligation;


				}
				
			}
            if (ritualObligationToRemove != null)
            {
				jobRitual.Ritual.activeObligations?.Remove(ritualObligationToRemove);
            }
			
			BodyPartRecord bodyPartRecord = suicide.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).FirstOrDefault((BodyPartRecord x) => x.def == BodyPartDefOf.Head); ;
			int num2 = Mathf.Clamp((int)suicide.health.hediffSet.GetPartHealth(bodyPartRecord) - 1, 1, 20);
			DamageInfo damageInfo = new DamageInfo(DamageDefOf.ExecutionCut, (float)num2, 999f, -1f, suicide, bodyPartRecord, null, DamageInfo.SourceCategory.ThingOrUnknown, null, false, true);
			suicide.TakeDamage(damageInfo);
			if (!suicide.Dead)
			{
				suicide.Kill(new DamageInfo?(damageInfo), null);
			}
			SoundDefOf.Execute_Cut.PlayOneShot(suicide);

			LookTargets lookTargets = jobRitual.selectedTarget;
			
			string str = outcome.label + " " + jobRitual.Ritual.Label;
			TaggedString taggedString = outcome.description.Formatted(jobRitual.Ritual.Label);
			ChoiceLetter let = LetterMaker.MakeLetter(str, taggedString, LetterDefOf.RitualOutcomePositive, lookTargets, null, null, null);
			Find.LetterStack.ReceiveLetter(let, null);
			
		}

	
	}
}
