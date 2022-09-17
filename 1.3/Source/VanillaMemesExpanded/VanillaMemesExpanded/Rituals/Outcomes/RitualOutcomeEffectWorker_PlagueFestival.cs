using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using Verse.Sound;


namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_PlagueFestival : RitualOutcomeEffectWorker_FromQuality
	{
		public RitualOutcomeEffectWorker_PlagueFestival()
		{
		}

		public RitualOutcomeEffectWorker_PlagueFestival(RitualOutcomeEffectDef def) : base(def)
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
			if (outcome.Positive)
			{
				foreach (KeyValuePair<Pawn, int> keyValuePair in totalPresence)
				{
					keyValuePair.Key.health.AddHediff(HediffDefOf.Plague);
				}

			}
			foreach (Pawn pawn in totalPresence.Keys)
			{

				base.GiveMemoryToPawn(pawn, outcome.memory, jobRitual);

			}
			LookTargets lookTargets = jobRitual.selectedTarget;
			
			string str = outcome.label + " " + jobRitual.Ritual.Label;
			TaggedString taggedString = outcome.description.Formatted(jobRitual.Ritual.Label);
			ChoiceLetter let = LetterMaker.MakeLetter(str, taggedString, LetterDefOf.RitualOutcomePositive, lookTargets, null, null, null);
			Find.LetterStack.ReceiveLetter(let, null);
			for (int i = 0; i < 20; i++)
			{
				IntVec3 c;
				CellFinder.TryFindRandomReachableCellNear(jobRitual.selectedTarget.Cell, jobRitual.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
				FilthMaker.TryMakeFilth(c, jobRitual.Map, ThingDefOf.Filth_CorpseBile);

			}
			SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(jobRitual.selectedTarget.Cell, jobRitual.Map, false));
		}

	
	}
}
