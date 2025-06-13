using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using Verse.Sound;

namespace VanillaMemesExpanded
{
	public class RitualBehaviorWorker_Orgy : RitualBehaviorWorker
	{
		private Sustainer soundPlaying;
		public RitualBehaviorWorker_Orgy()
		{
		}

		public RitualBehaviorWorker_Orgy(RitualBehaviorDef def) : base(def)
		{
		}


		public override void PostCleanup(LordJob_Ritual ritual)
		{
			foreach(Pawn p in ritual.Map.mapPawns.FreeColonistsAndPrisonersSpawned)
            {
                Hediff hediff = p.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.VME_Naked);
                if (hediff != null) { p.health.RemoveHediff(hediff); }
                p.Drawer.renderer.SetAllGraphicsDirty();
                PortraitsCache.SetDirty(p);
				GlobalTextureAtlasManager.TryMarkPawnFrameSetDirty(p);

			}
		}
		public override void Tick(LordJob_Ritual ritual)
		{
			base.Tick(ritual);
			if (ritual.StageIndex == 1)
			{
				if (this.soundPlaying == null || this.soundPlaying.Ended)
				{
					TargetInfo selectedTarget = ritual.selectedTarget;
					this.soundPlaying = InternalDefOf.VME_RitualSustainer_Orgy.TrySpawnSustainer(SoundInfo.InMap(new TargetInfo(selectedTarget.Cell, selectedTarget.Map, false), MaintenanceType.PerTick));
				}
				Sustainer sustainer = this.soundPlaying;
				if (sustainer == null)
				{
					return;
				}
				sustainer.Maintain();
			}
		}
	}
}
