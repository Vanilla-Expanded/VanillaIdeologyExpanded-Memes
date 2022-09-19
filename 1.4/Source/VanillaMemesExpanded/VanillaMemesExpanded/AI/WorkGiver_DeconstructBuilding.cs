using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace VanillaMemesExpanded
{
	public class WorkGiver_DeconstructBuilding : WorkGiver_Scanner
	{
		public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
		{
			return PawnCollectionClass.objectsToDeconstruct_InMap;
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
			
			bool result;
			if (t == null || t.IsBurning())
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
			return new Job(InternalDefOf.VME_DeconstructBuilding, t);
		}
	}
}
