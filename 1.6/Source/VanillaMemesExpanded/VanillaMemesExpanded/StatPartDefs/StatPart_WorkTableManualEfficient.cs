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
	public class StatPart_WorkTableManualEfficient : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				val *= 1.25f;
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				float nonAutomatedWorkTableWorkSpeedFactor = 1.25f;
				return "VME_AutomationManualFaster".Translate() + ": x" + nonAutomatedWorkTableWorkSpeedFactor.ToStringPercent();
			}
			return null;
		}

		public static bool Applies(Thing th)
		{
            if (Find.IdeoManager.classicMode) return false;
           
			return StaticCollections.manualWorkbenches.Contains(th.def) && Current.Game.World.factionManager.OfPlayer.ideos.PrimaryIdeo.GetPrecept(InternalDefOf.VME_CraftingSpeed_FasterForManual) !=null;
		}
	}
}
