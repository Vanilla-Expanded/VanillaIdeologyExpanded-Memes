using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	[StaticConstructorOnStartup]
	public class CompPermanentFireOverlay : CompFireOverlayBase
	{
		public new CompProperties_PermanentFireOverlay Props
		{
			get
			{
				return (CompProperties_PermanentFireOverlay)this.props;
			}
		}

		public override void PostDraw()
		{
			base.PostDraw();
			
			Vector3 loc = this.parent.TrueCenter() + (Vector3.forward * Props.upOffset);
			loc.y = AltitudeLayer.MoteOverhead.AltitudeFor();
			CompFireOverlayCool.FireGraphic.Draw(loc, Rot4.North, this.parent, 0f);
		}

		public override void CompTick()
		{
			
			if (this.startedGrowingAtTick < 0)
			{
				this.startedGrowingAtTick = GenTicks.TicksAbs;
			}
			if (GenTicks.TicksAbs % FireGlowIntervalTicks == 0)
			{
				FleckMaker.ThrowFireGlow(this.parent.TrueCenter(), this.parent.Map, 1f);
			}
		}

		public const int FireGlowIntervalTicks = 30;
	}
}
