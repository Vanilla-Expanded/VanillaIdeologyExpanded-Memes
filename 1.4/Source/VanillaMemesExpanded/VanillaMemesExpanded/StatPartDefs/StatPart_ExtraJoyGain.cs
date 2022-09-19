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
	public class StatPart_ExtraJoyGain : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				val *= 0.5f;
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				float recreation = 0.5f;
				return "VME_ExtraJoyGain".Translate() + ": x" + recreation.ToStringPercent();
			}
			return null;
		}

		public static bool Applies(Thing th)
		{
			
			
			return Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Recreation_Loved)!=null;
		}
	}
}
