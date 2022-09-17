using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using Verse.Sound;
using UnityEngine;


namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_LeadershipChallenge : RitualOutcomeEffectWorker_FromQuality
	{
		public RitualOutcomeEffectWorker_LeadershipChallenge()
		{
		}

		public RitualOutcomeEffectWorker_LeadershipChallenge(RitualOutcomeEffectDef def) : base(def)
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
			Pawn duelist1 = jobRitual.PawnWithRole("duelist1");
			Pawn duelist2 = jobRitual.PawnWithRole("duelist2");

			Precept_Role precept_role = Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo.GetPrecept(PreceptDefOf.IdeoRole_Leader) as Precept_Role;
			Pawn leader = precept_role.ChosenPawnSingle();

			Pawn winner = ((LordJob_Ritual_Duel)jobRitual).duelists.Find((Pawn d) => !d.Dead);

			if(winner!= leader)
            {
				PawnCollectionClass.pawnThatIsTheLeaderNow = winner;
				Current.Game.GetComponent<GameComponent_BestMeleeLeaderTracker>().pawnThatIsTheLeaderNow = winner;

				precept_role.Assign(winner, true);
				
			}











			LookTargets lookTargets = jobRitual.selectedTarget;

			string str = outcome.label + " " + jobRitual.Ritual.Label;
			TaggedString taggedString = outcome.description.Formatted(jobRitual.Ritual.Label);
			ChoiceLetter let = LetterMaker.MakeLetter(str, taggedString, LetterDefOf.RitualOutcomePositive, lookTargets, null, null, null);
			Find.LetterStack.ReceiveLetter(let, null);

		}


	}
}
