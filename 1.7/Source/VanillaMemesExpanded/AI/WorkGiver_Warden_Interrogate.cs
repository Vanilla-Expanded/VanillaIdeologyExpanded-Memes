using RimWorld;
using Verse;
using Verse.AI;

namespace VanillaMemesExpanded
{
    public class WorkGiver_Warden_Interrogate : WorkGiver_Warden
    {
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!ShouldTakeCareOfPrisoner(pawn, t, forced)) return null;
            var p = (Pawn)t;
            if (p.guest.ExclusiveInteractionMode == InternalDefOf.VFEA_Interrogate && p.guest.ScheduledForInteraction && p.guest.IsPrisoner && !p.Downed &&
                pawn.health.capacities.CapableOf(PawnCapacityDefOf.Talking) && pawn.CanReserveAndReach(t, PathEndMode.Touch, Danger.Some) && p.Awake())
                return JobMaker.MakeJob(InternalDefOf.VFEA_PrisonerInterrogate, t);
            return null;
        }
    }
}