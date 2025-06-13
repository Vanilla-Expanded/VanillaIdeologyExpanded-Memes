using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace VanillaMemesExpanded
{
	public class WorkGiver_MaintainInsectNest : WorkGiver_Scanner
	{
		public override ThingRequest PotentialWorkThingRequest
		{
			get
			{
				return ThingRequest.ForDef(InternalDefOf.VME_InsectNest);
			}
		}

		public override PathEndMode PathEndMode
		{
			get
			{
				return PathEndMode.Touch;
			}
		}

		public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			Building_InsectNest building = t as Building_InsectNest;
			Precept_Role precept_role;
			bool correctRole = pawn.Ideo?.HasPrecept(InternalDefOf.VME_IdeoRole_InsectoidHerder)==true && (precept_role = pawn.Ideo?.GetPrecept(InternalDefOf.VME_IdeoRole_InsectoidHerder) as Precept_Role) !=null && precept_role.ChosenPawnSingle() == pawn;
			bool result;
			if (t == null || t.IsBurning()||!correctRole|| building.maintenance>50)
			{

				result = false;
			}
			else
			{

				if (!t.IsForbidden(pawn))
				{

					if (pawn.CanReserve(t, 1, -1, null, forced))
					{

						return true;
					}
				}
				result = false;
			}
			return result;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return new Job(InternalDefOf.VME_MaintainInsectNest, t);
		}
	}
}
