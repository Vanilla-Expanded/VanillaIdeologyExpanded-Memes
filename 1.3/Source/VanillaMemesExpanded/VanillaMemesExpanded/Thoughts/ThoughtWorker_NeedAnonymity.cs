using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_NeedAnonymity : ThoughtWorker
	{
		protected override ThoughtState CurrentStateInternal(Pawn p)
		{
			if (p.needs?.TryGetNeed<Need_Anonymity>() == null)
			{
				return ThoughtState.Inactive;
			}

			if (p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Anonymity_Required)!=true)
			{
				return ThoughtState.Inactive;
			}
			Need_Anonymity need = p.needs.TryGetNeed<Need_Anonymity>();
			switch (need.CurCategory)
			{
				case AnonymityNeedCategory.AnonymityViolated:
					return ThoughtState.ActiveAtStage(0);
				case AnonymityNeedCategory.AnonymityCompromised:
					return ThoughtState.ActiveAtStage(1);
				case AnonymityNeedCategory.AnonymityThreatened:
					return ThoughtState.ActiveAtStage(2);
				case AnonymityNeedCategory.Anonymous:
					return ThoughtState.Inactive;
					
				case AnonymityNeedCategory.CompletelyAnonymous:
					return ThoughtState.ActiveAtStage(3);
				
				default:
					throw new NotImplementedException();
			}
		}
	}
}
