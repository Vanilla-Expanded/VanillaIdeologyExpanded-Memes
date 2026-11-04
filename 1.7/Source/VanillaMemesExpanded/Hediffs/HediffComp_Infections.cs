using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class HediffComp_Infections : HediffComp
	{

        public int tickInterval = 60000;

        public override void CompPostMake()
        {
            Pawn pawn = this.parent.pawn;
            if (pawn.ideo != null && pawn.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_OrganUse_PostMortem)!=true)
            {
                base.Pawn.health.AddHediff(HediffDefOf.WoundInfection, this.parent.Part, null, null);
               
            }
            base.CompPostMake();
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (this.parent.pawn.IsHashIntervalTick(tickInterval)){
                Pawn pawn = this.parent.pawn;
                if (pawn.ideo != null && pawn.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_OrganUse_PostMortem) != true)
                {
                    base.Pawn.health.AddHediff(HediffDefOf.WoundInfection, this.parent.Part, null, null);

                }

            }
           

        }



    }
}
