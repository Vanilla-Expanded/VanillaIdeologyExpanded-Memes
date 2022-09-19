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
	public class StatPart_WorkTableManualUnefficient : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				val *= 0.75f;
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				float nonAutomatedWorkTableWorkSpeedFactor = 0.75f;
				return "VME_AutomationManual".Translate() + ": x" + nonAutomatedWorkTableWorkSpeedFactor.ToStringPercent();
			}
			return null;
		}

		public static bool Applies(Thing th)
		{

			CompPowerTrader compPowerTrader = th.TryGetComp<CompPowerTrader>();
			return compPowerTrader == null && Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_CraftingSpeed_SlowerForManual)!=null;
		}
	}
}
