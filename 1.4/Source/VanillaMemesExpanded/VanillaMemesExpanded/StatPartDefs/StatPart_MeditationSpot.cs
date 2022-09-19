using System;
using RimWorld;
using System.Reflection;
using Verse;


namespace VanillaMemesExpanded
{
	public class StatPart_MeditationSpot : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				val *= 1.20f;
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				float artWorkTableWorkSpeedFactor = 1.20f;
				return "VME_MeditationSpotExtra".Translate() + ": x" + artWorkTableWorkSpeedFactor.ToStringPercent();
			}
			return null;
		}

		public static bool Applies(Thing th)
		{

			
			return Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(DefDatabase<PreceptDef>.GetNamedSilentFail("VME_Meditation_Exquisite")) != null;
		}
	}
}
