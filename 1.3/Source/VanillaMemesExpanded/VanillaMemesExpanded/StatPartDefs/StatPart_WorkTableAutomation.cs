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
	public class StatPart_WorkTableAutomation : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				val *= 1.5f;
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				float automatedWorkTableWorkSpeedFactor = 1.5f;
				return "VME_Automation".Translate() + ": x" + automatedWorkTableWorkSpeedFactor.ToStringPercent();
			}
			return null;
		}

		public static bool Applies(Thing th)
		{
			
			CompPowerTrader compPowerTrader = th.TryGetComp<CompPowerTrader>();
			return compPowerTrader != null && Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_AutomationEfficiency_Increased)!=null;
		}
	}
}
