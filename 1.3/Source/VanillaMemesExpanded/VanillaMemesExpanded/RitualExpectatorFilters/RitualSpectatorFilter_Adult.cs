using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded 
{ 

	
	public class RitualSpectatorFilter_Adult : RitualSpectatorFilter
	{
		public override bool Allowed(Pawn p)
		{
			return p.ageTracker.AgeBiologicalYears >= 18;
		}
	}
}


