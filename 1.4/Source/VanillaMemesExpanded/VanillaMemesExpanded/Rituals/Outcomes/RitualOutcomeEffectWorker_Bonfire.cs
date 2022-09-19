using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_Bonfire : RitualOutcomeEffectWorker_FromQuality
	{
		public override bool SupportsAttachableOutcomeEffect
		{
			get
			{
				return false;
			}
		}

		public RitualOutcomeEffectWorker_Bonfire()
		{
		}

		public RitualOutcomeEffectWorker_Bonfire(RitualOutcomeEffectDef def) : base(def)
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

			GameComponent_BonfireTracker comp = Current.Game.GetComponent<GameComponent_BonfireTracker>();
			foreach (Pawn pawn in totalPresence.Keys)
			{
				
				base.GiveMemoryToPawn(pawn, outcome.memory, jobRitual);
				if (comp != null)
                {
					switch (outcome.positivityIndex)
					{
						case 1:
							comp.AddColonistAndTicksToCountdown(pawn,10000);
							
							break;
						case 2:
							comp.AddColonistAndTicksToCountdown(pawn, 60000);

							break;
						case 3:
							comp.AddColonistAndTicksToCountdown(pawn, 180000);

							break;
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

			Building altar = jobRitual.selectedTarget.Thing as Building;
			IntVec3 loc = altar.Position;
			Map map = altar.Map;
			altar.Destroy();
			ThingDef newThing = InternalDefOf.VME_BonfireAfterRitual;
			Thing bonfire = GenSpawn.Spawn(newThing, loc, map, WipeMode.Vanish);
			bonfire.SetFaction(Faction.OfPlayer);

		}
	}
}
