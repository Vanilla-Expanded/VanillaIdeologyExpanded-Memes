using System;
using Verse;
using RimWorld;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
	public class RitualObligationTrigger_StrongerLeader : RitualObligationTrigger
	{

		public int tickCounter = tickInterval;
		public const int tickInterval = 6000;
		private static List<Pawn> existingObligations = new List<Pawn>();

		public override void Tick()
		{
			tickCounter++;
			if ((tickCounter > tickInterval))
			{

				WorldComponent_BestMeleeLeaderTracker comp = WorldComponent_BestMeleeLeaderTracker.Instance;
				if (this.ritual.activeObligations != null)
				{
					List<RitualObligation> obligationsToRemove = new List<RitualObligation>();
					foreach (RitualObligation ritualObligation in this.ritual.activeObligations)
					{
						Pawn pawn = ritualObligation.targetA.Thing as Pawn;
						if (pawn != null && (pawn.Dead|| comp.mostSkilledPawn == comp.currentBestMeleeLeaderPawn))
						{
							obligationsToRemove.Add(ritualObligation);


						}
						else
						{
							existingObligations.Add(ritualObligation.targetA.Thing as Pawn);
						}
					}
					foreach (RitualObligation ritualObligationToRemove in obligationsToRemove)
					{
						this.ritual.activeObligations.Remove(ritualObligationToRemove);
					}

				}

				
					if (comp.mostSkilledPawn!=null&&!existingObligations.Contains(comp.mostSkilledPawn) && comp.mostSkilledPawn.Ideo != null)
					{
						
						if (comp.mostSkilledPawn != comp.currentBestMeleeLeaderPawn)
						{
							this.ritual.AddObligation(new RitualObligation(this.ritual, comp.mostSkilledPawn, false)
							{
								sendLetter = true
							});
						}
					}
				
				tickCounter = 0;
				existingObligations.Clear();
			}


		}

	}
}