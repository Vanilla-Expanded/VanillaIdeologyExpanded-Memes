using System;
using UnityEngine;
using Verse;

namespace RimWorld
{
	[StaticConstructorOnStartup]
	public class CompFireOverlayCool : CompFireOverlay
	{
		

		public override void PostDraw()
		{
			base.PostDraw();
			if (this.refuelableComp != null && !this.refuelableComp.HasFuel)
			{
				return;
			}
			Vector3 drawPos = this.parent.DrawPos;
			drawPos.y += 0.04054054f;
			CompFireOverlayCool.FireGraphic.Draw(drawPos, Rot4.North, this.parent, 0f);
		}

		

		public static new readonly Graphic FireGraphic = GraphicDatabase.Get<Graphic_Flicker>("Motes/LargeFire", ShaderDatabase.TransparentPostLight, Vector2.one, Color.white);
	}
}
