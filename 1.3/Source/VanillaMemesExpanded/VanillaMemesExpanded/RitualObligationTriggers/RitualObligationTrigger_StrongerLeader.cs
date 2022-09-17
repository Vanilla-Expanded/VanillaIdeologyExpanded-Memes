using System;
using Verse;
using RimWorld;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
	public class RitualObligationTrigger_StrongerLeader : RitualObligationTrigger
	{

		public int tickCounter = 0;
		public int tickInterval = 6000;
		private static List<Pawn> existingObligations = new List<Pawn>();

		public override void Tick()
		{
			tickCounter++;
			if ((tickCounter > tickInterval))
			{

				GameComponent_BestMeleeLeaderTracker comp = Current.Game.GetComponent<GameComponent_BestMeleeLeaderTracker>();
				if (this.ritual.activeObligations != null)
				{
					List<RitualObligation> obligationsToRemove = new List<RitualObligation>();
					foreach (RitualObligation ritualObligation in this.ritual.activeObligations)
					{
						Pawn pawn = ritualObligation.targetA.Thing as Pawn;
						if (pawn != null && (pawn.Dead|| comp.mostSkilledPawn == comp.pawnThatIsTheLeaderNow))
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
						
						if (comp.mostSkilledPawn != comp.pawnThatIsTheLeaderNow)
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