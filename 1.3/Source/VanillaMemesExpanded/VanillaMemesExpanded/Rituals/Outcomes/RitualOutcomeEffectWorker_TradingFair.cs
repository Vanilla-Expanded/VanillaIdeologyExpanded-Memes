using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using System.Linq;

namespace VanillaMemesExpanded
{
	public class RitualOutcomeEffectWorker_TradingFair : RitualOutcomeEffectWorker_FromQuality
	{
		public override bool SupportsAttachableOutcomeEffect
		{
			get
			{
				return false;
			}
		}

		public RitualOutcomeEffectWorker_TradingFair()
		{
		}

		public RitualOutcomeEffectWorker_TradingFair(RitualOutcomeEffectDef def) : base(def)
		{
		}

		private static int AmountSendableSilver(Map map)
		{
			return (from t in TradeUtility.AllLaunchableThingsForTrade(map)
					where t.def == ThingDefOf.Silver
					select t).Sum((Thing t) => t.stackCount);
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

			TargetInfo selectedTarget = jobRitual.selectedTarget;
			if (AmountSendableSilver(selectedTarget.Map) < 200)
			{
				foreach (Pawn pawn in totalPresence.Keys)
				{
					ThoughtDef cheapTradeMemory = InternalDefOf.VME_CheapTradingFair;

					pawn.needs.mood.thoughts.memories.TryGainMemory(cheapTradeMemory,null,jobRitual.Ritual);

				}
			}
            else
            {
				TradeUtility.LaunchThingsOfType(ThingDefOf.Silver, 200, selectedTarget.Map, null);
			}
           
				int numberOfCaravans = 0;
				switch (outcome.positivityIndex)
				{

					case 1:
						numberOfCaravans=1;
						break;
					case 2:
						numberOfCaravans = 3;
						break;
					case 3:
						numberOfCaravans = 6;
						break;

				}
				for(int i =0;i< numberOfCaravans; i++)
                {
					IncidentDef incidentDef = IncidentDefOf.TraderCaravanArrival;
					IncidentParms parms = StorytellerUtility.DefaultParmsNow(incidentDef.category, selectedTarget.Map);
					incidentDef.Worker.TryExecute(parms);
					
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
