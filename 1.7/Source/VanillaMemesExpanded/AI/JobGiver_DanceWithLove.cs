// RimWorld.JobGiver_Dance
using RimWorld;
using Verse;
using Verse.AI;
namespace VanillaMemesExpanded
{

	public class JobGiver_DanceWithLove : ThinkNode_JobGiver
	{
		public IntRange ticksRange = new IntRange(300, 600);

		public override ThinkNode DeepCopy(bool resolve = true)
		{
			JobGiver_DanceWithLove obj = (JobGiver_DanceWithLove)base.DeepCopy(resolve);
			obj.ticksRange = ticksRange;
			return obj;
		}

		protected override Job TryGiveJob(Pawn pawn)
		{
			Job job = JobMaker.MakeJob(InternalDefOf.VME_DanceWithLove);
			job.expiryInterval = ticksRange.RandomInRange;
			return job;
		}
	}
}

