using System;
using Verse;
using RimWorld;


namespace VanillaMemesExpanded
{
    public class PreceptComp_AddDessertNeed : PreceptComp
    {
        public override void Notify_MemberTookAction(HistoryEvent ev, Precept precept, bool canApplySelfTookThoughts)
        {
            
			if (ev.def != this.eventDef)
			{
				return;
			}
			Pawn pawn = ev.args.GetArg<Pawn>(HistoryEventArgsNames.Doer);
			if (pawn.needs != null)
			{
				Need_Desserts need = pawn.needs.TryGetNeed<Need_Desserts>();
				need.DessertTaken(needGain);
				need.CurLevel += needGain;
			}
		}
		public HistoryEventDef eventDef;
		public float needGain;
		public bool onlyForNonSlaves;
	}


}
