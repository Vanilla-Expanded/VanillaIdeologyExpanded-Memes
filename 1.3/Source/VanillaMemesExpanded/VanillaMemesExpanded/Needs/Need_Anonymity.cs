using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class Need_Anonymity : Need
	{
		public override float CurInstantLevel
		{
			get
			{
				if (this.lastAnonymityUseTick >= Find.TickManager.TicksGame - 10)
				{
					return Mathf.Clamp01(this.lastAnonymityUsed);
				}
				return 0f;
			}
		}
		public AnonymityNeedCategory CurCategory
		{
			get
			{
				if (this.CurLevel < 0.1f)
				{
					return AnonymityNeedCategory.AnonymityViolated;
				}
				if (this.CurLevel < 0.2f)
				{
					return AnonymityNeedCategory.AnonymityCompromised;
				}
				if (this.CurLevel < 0.3f)
				{
					return AnonymityNeedCategory.AnonymityThreatened;
				}
				if (this.CurLevel < 0.7f)
				{
					return AnonymityNeedCategory.Anonymous;
				}
				
				return AnonymityNeedCategory.CompletelyAnonymous;
			}
		}

		public override bool ShowOnNeedList
		{
			get
			{

				if (this.pawn.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Anonymity_Required)==true)
				{
					return true ;
				}
				else return false;


			}
		}



		public override void NeedInterval()
		{
			if (!this.IsFrozen)
			{
				bool flag = false;
				List<Apparel> wornApparel = this.pawn.apparel.WornApparel;
				for (int i = 0; i < wornApparel.Count; i++)
				{
					if (wornApparel[i].def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.FullHead))
					{
						flag = true;
					}
				}

				if (flag)
				{

					this.CurLevel += this.AnonymityFallPerTick * 150f;
				}else this.CurLevel -= this.AnonymityFallPerTick * 150f;




			}
		}

		private float AnonymityFallPerTick
		{
			get
			{
				return this.def.fallPerDay / 60000f;
			}
		}

		public Need_Anonymity(Pawn pawn) : base(pawn)
		{

		}

		public void AnonymityTaken(float anonymity)
		{
			this.lastAnonymityUsed = anonymity;
			this.lastAnonymityUseTick = Find.TickManager.TicksGame;
		}

		public float lastAnonymityUsed;

		public int lastAnonymityUseTick;




	}
}
