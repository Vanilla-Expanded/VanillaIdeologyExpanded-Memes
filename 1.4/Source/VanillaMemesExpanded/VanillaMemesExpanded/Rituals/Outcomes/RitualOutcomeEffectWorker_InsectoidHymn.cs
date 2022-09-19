using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using Verse.Sound;
using Verse.AI.Group;
using Verse.AI;


namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_InsectoidHymn : RitualOutcomeEffectWorker_FromQuality
	{
		public RitualOutcomeEffectWorker_InsectoidHymn()
		{
		}

		public RitualOutcomeEffectWorker_InsectoidHymn(RitualOutcomeEffectDef def) : base(def)
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
			Pawn chanter = jobRitual.PawnWithRole("chanter");

			Precept_Role precept_role = chanter.ideo.Ideo.GetPrecept(InternalDefOf.VME_IdeoRole_InsectoidHerder) as Precept_Role;
			Pawn leader = precept_role.ChosenPawnSingle();

			if (outcome.Positive)
			{
				
				leader.abilities?.GetAbility(InternalDefOf.VME_ConstructANest,true)?.StartCooldown(30);

			}

            if (outcome.positivityIndex == 2)
            {
				foreach (Lord lord in Find.CurrentMap.lordManager.lords)
				{

					if (lord.faction == Faction.OfInsects)
					{

						for (int i = 0; i < lord.ownedPawns.Count; i++)
						{
							Pawn pawn = lord.ownedPawns[i];

							pawn.mindState.duty = new PawnDuty(DutyDefOf.ExitMapRandom);

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
