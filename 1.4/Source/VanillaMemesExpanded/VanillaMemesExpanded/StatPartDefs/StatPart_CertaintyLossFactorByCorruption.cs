using System;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaMemesExpanded
{
	public class StatPart_CertaintyLossFactorByCorruption : StatPart
	{

		public bool certaintyNull = false;

		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				if (certaintyNull)
				{
					val *= 1000f;
				}
				else
				{
					val *= 0;
				}
				
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				if (certaintyNull)
                {
					float certaintyloss = 1000f;
					return "VME_ExtraCertaintyLoss".Translate() + ": x" + certaintyloss.ToStringPercent();
				}
                else
                {
					float certaintyloss = 0;
					return "VME_NullCertaintyLoss".Translate() + ": x" + certaintyloss.ToStringPercent();
				}
				
			}
			return null;
		}

		public bool Applies(Thing th)
		{
			Pawn pawn = th as Pawn;
            if (pawn != null && pawn.Ideo?.HasMeme(InternalDefOf.VME_Structure_ChthonianCult)==true)
            {
				if (pawn.needs != null)
				{
					Need_Corruption need = pawn.needs.TryGetNeed<Need_Corruption>();
					if (need?.CurLevel == 0) {
						certaintyNull = true;
						return true;
					}
					else if (need?.CurLevel >= 0)
					{
						
						certaintyNull = false;
						return true;
					}


				}
			}
			return false;
		}
	}
}
