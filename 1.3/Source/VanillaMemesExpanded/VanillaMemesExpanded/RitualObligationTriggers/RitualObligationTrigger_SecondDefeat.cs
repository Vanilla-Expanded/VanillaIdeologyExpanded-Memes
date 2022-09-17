using System;
using Verse;
using RimWorld;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
	public class RitualObligationTrigger_SecondDefeat : RitualObligationTrigger
	{

        public int tickCounter = 0;
        public int tickInterval = 6000;
		private static List<Pawn> existingObligations = new List<Pawn>();

		public override void Tick()
        {
            tickCounter++;
            if ((tickCounter > tickInterval))
            {


				if (this.ritual.activeObligations != null) {

					List<RitualObligation> obligationsToRemove = new List<RitualObligation>();

					foreach (RitualObligation ritualObligation in this.ritual.activeObligations)
					{
						Pawn pawn = ritualObligation.targetA.Thing as Pawn;
						if (pawn!=null&&pawn.Dead) {
							obligationsToRemove.Add(ritualObligation);
							

						}
						else {
							existingObligations.Add(ritualObligation.targetA.Thing as Pawn);
						}
					}
					foreach (RitualObligation ritualObligationToRemove in obligationsToRemove)
					{
						this.ritual.activeObligations.Remove(ritualObligationToRemove);
					}

				} 
				
				foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
				{
					if (!existingObligations.Contains(pawn) && pawn.Ideo != null)
					{
						if (pawn.Map?.GetComponent<MapComponent_PawnsInMapDesiringRitualSuicide>()?.pawnsDesiringSuicide.Contains(pawn)==true)
						{
							ritual.AddObligation(new RitualObligation(this.ritual, pawn, false)
							{
								sendLetter = true
							});
						}
					}
				}
				tickCounter = 0;
				existingObligations.Clear();
			}
			

		}
		
	}
}