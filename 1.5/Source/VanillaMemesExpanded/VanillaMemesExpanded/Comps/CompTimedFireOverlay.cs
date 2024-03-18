using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	[StaticConstructorOnStartup]
	public class CompTimedFireOverlay : CompFireOverlayBase
	{
		public new CompProperties_TimedFireOverlay Props
		{
			get
			{
				return (CompProperties_TimedFireOverlay)this.props;
			}
		}

		public override void PostDraw()
		{
			base.PostDraw();
			LordJob_Ritual lordJob_Ritual = this.parent.TargetOfRitual();
			if (lordJob_Ritual == null)
			{
				return;
			}
			if (lordJob_Ritual.StageIndex < this.Props.minRitualProgress)
			{
				return;
			}
			Vector3 loc = this.parent.TrueCenter()+(Vector3.forward*Props.upOffset);
			loc.y = AltitudeLayer.MoteOverhead.AltitudeFor();
			CompFireOverlayCool.FireGraphic.Draw(loc, Rot4.North, this.parent, 0f);
		}

		public override void CompTick()
		{
			LordJob_Ritual lordJob_Ritual = this.parent.TargetOfRitual();
			if (lordJob_Ritual == null)
			{
				return;
			}
			if (lordJob_Ritual.StageIndex < this.Props.minRitualProgress)
			{
				return;
			}
			if (this.startedGrowingAtTick < 0)
			{
				this.startedGrowingAtTick = GenTicks.TicksAbs;
			}
			if (GenTicks.TicksAbs % 30 == 0)
			{
				FleckMaker.ThrowFireGlow(this.parent.TrueCenter(), this.parent.Map, 1f);
			}
		}

		public const int FireGlowIntervalTicks = 30;
	}
}
