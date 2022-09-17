using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class Need_Corruption : Need
	{
		public override float CurInstantLevel
		{
			get
			{
				if (this.lastCorruptionUseTick >= Find.TickManager.TicksGame - 10)
				{
					return Mathf.Clamp01(this.lastCorruptionUsed);
				}
				return 0f;
			}
		}
		public CorruptionNeedCategory CurCategory
		{
			get
			{
				if (this.CurLevel < 0.1f)
				{
					return CorruptionNeedCategory.Uncorrupted;
				}
				if (this.CurLevel < 0.2f)
				{
					return CorruptionNeedCategory.CorruptionCompromised;
				}
				if (this.CurLevel < 0.3f)
				{
					return CorruptionNeedCategory.CorruptionThreatened;
				}
				if (this.CurLevel < 0.7f)
				{
					return CorruptionNeedCategory.Corruption;
				}

				return CorruptionNeedCategory.CompletelyCorrupted;
			}
		}

		public override bool ShowOnNeedList
		{
			get
			{

				if (this.pawn.Ideo?.HasPrecept(InternalDefOf.VME_Corruption_Essential)!=true)
				{
					return false;
				}
				else return true;


			}
		}



		public override void NeedInterval()
		{
			if (!this.IsFrozen)
			{

                if (!PawnCollectionClass.colonist_obelisk_tracker.ContainsKey(this.pawn) || !PawnCollectionClass.colonist_obelisk_tracker[this.pawn])
                {
					this.CurLevel -= this.CorruptionFallPerTick;
                }
				




			}
		}

		private float CorruptionFallPerTick
		{
			get
			{
				if (this.pawn.Ideo?.HasPrecept(InternalDefOf.VME_Corruption_Essential) != true)
				{
					return 0;
				}
				else return this.def.fallPerDay / 60000f;


			}
		}

		public Need_Corruption(Pawn pawn) : base(pawn)
		{

		}

		public void CorruptionTaken(float corruption)
		{
			this.lastCorruptionUsed = corruption;
			this.lastCorruptionUseTick = Find.TickManager.TicksGame;
		}

		public float lastCorruptionUsed;

		public int lastCorruptionUseTick;




	}
}
