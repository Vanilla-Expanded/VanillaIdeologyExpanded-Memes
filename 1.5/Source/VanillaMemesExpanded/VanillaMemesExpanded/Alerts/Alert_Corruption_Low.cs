
using RimWorld;
using System;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace VanillaMemesExpanded
{
	public class Alert_Corruption_Low : Alert
	{
		

		private List<Pawn> corruptedPawnsResult = new List<Pawn>();

		private List<Pawn> CorruptedPawns
		{
			get
			{
				corruptedPawnsResult.Clear();
				foreach (Pawn pawn in PawnsFinder.AllMaps_FreeColonistsSpawned)
				{
					if (pawn.needs.TryGetNeed<Need_Corruption>() != null && (pawn.needs.TryGetNeed<Need_Corruption>().CurLevel<0.1f )
						&& pawn.Ideo?.HasPrecept(InternalDefOf.VME_Corruption_Essential)==true)
					{
						corruptedPawnsResult.Add(pawn);
					}
				}
				return corruptedPawnsResult;
			}
		}

		public Alert_Corruption_Low()
		{
			defaultLabel = "VEM_LowCorruption".Translate();
			defaultPriority = AlertPriority.Medium;
		}

		public override AlertReport GetReport()
		{
			return AlertReport.CulpritsAre(CorruptedPawns);
		}

		public override TaggedString GetExplanation()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Pawn pawn = null;
			foreach (Pawn coruptedPawn in CorruptedPawns)
			{
				stringBuilder.AppendLine("   " + coruptedPawn.Label);
				if (pawn == null)
				{
					pawn = coruptedPawn;
				}
			}
		
			return "VEM_LowCorruptionDesc".Translate(stringBuilder.ToString().TrimEndNewlines(), pawn.Named("PAWN"));
		}
	}
}

