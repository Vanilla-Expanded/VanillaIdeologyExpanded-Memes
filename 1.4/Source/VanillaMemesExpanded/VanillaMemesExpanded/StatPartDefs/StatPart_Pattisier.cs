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
	public class StatPart_Pattisier : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				val *= 1.2f;
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing && Applies(req.Thing))
			{
				float recreation = 1.2f;
				return "VME_ExtraPatissierCooking".Translate() + ": x" + recreation.ToStringPercent();
			}
			return null;
		}

		public static bool Applies(Thing th)
		{
			Pawn pawn = th as Pawn;
			Precept_Role precept_role;
			if (pawn != null && pawn.Ideo?.HasPrecept(DefDatabase<PreceptDef>.GetNamedSilentFail("VME_IdeoRole_Patissier")) == true)
			{
				if ((precept_role = pawn.Ideo?.GetPrecept(DefDatabase<PreceptDef>.GetNamedSilentFail("VME_IdeoRole_Patissier")) as Precept_Role) != null && precept_role.ChosenPawnSingle()==pawn)
                {
					return true;
                }
			}
			return false;
		}
	}
	}

