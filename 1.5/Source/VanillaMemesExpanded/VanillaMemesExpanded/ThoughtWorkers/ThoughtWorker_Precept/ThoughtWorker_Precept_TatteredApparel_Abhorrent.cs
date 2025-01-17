using System;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_TatteredApparel_Abhorrent : ThoughtWorker_Precept
	{
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			bool flag = false;
			List<Apparel> wornApparel = p.apparel.WornApparel;
			for (int i = 0; i < wornApparel.Count; i++)
			{
				if (wornApparel[i].def.useHitPoints && !p.apparel.IsLocked(wornApparel[i]) && wornApparel[i].def.apparel.careIfDamaged)
				{
					float num = (float)wornApparel[i].HitPoints / (float)wornApparel[i].MaxHitPoints;
					
					if (num < 0.5f)
					{
						flag = true; 
					}
				}
			}

			if (flag)
			{

				return ThoughtState.ActiveAtStage(0);
			}
			else return false;






		}
	}
}