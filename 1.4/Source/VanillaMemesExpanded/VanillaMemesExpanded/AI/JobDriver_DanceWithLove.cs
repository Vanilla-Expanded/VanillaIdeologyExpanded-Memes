// RimWorld.JobDriver_Dance
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
namespace VanillaMemesExpanded
{

	public class JobDriver_DanceWithLove : JobDriver
	{
		private bool jumping;

		private int moveChangeInterval = 240;

		public int AgeTicks => Find.TickManager.TicksGame - startTick;

		public override Vector3 ForcedBodyOffset
		{
			get
			{
				float num = Mathf.Sin((float)AgeTicks / 60f * 8f);
				if (jumping)
				{
					float z = Mathf.Max(Mathf.Pow((num + 1f) * 0.5f, 2f) * 0.2f - 0.06f, 0f);
					return new Vector3(0f, 0f, z);
				}
				float num2 = Mathf.Sign(num);
				return new Vector3(EaseInOutQuad(Mathf.Abs(num) * 0.6f) * 0.09f * num2, 0f, 0f);
				float EaseInOutQuad(float v)
				{
					if (!((double)v < 0.5))
					{
						return 1f - Mathf.Pow(-2f * v + 2f, 4f) / 2f;
					}
					return 8f * v * v * v * v;
				}
			}
		}

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return true;
		}

		public override void Notify_Starting()
		{
			base.Notify_Starting();
			pawn.Rotation = Rot4.Random;
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			if (ModLister.CheckIdeology("Dance job"))
			{
				Toil toil = new Toil();
				jumping = Rand.Bool;
				toil.tickAction = delegate
				{
					if (AgeTicks % moveChangeInterval == 0)
					{
						jumping = !jumping;
					}
					if (AgeTicks % 120 == 0 && !jumping)
					{
						pawn.Rotation = Rot4.Random;
					}
					if (pawn.IsHashIntervalTick(500))
					{
						FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.Heart);
					}
				};
				toil.socialMode = RandomSocialMode.SuperActive;
				toil.defaultCompleteMode = ToilCompleteMode.Never;
				toil.handlingFacing = true;
				yield return toil;
			}
		}
	}

}
