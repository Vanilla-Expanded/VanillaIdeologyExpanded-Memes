using System;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Illness : ThoughtWorker_Precept
	{
        public const int tickInterval = 300000; //5 days


        protected override ThoughtState ShouldHaveThought(Pawn p)
		{
            if (p.health.hediffSet.AnyHediffMakesSickThought)
            {
                return ThoughtState.ActiveAtStage(0);
            }
            else if (WorldComponent_AgeAndIllnessTracker.Instance.colonist_illness_tracker.ContainsKey(p) && WorldComponent_AgeAndIllnessTracker.Instance.colonist_illness_tracker[p] > tickInterval)
            {
                return ThoughtState.ActiveAtStage(1); 
            }

            return false;
		}
	}
}
