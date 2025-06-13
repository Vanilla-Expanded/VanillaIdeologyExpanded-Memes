using System;
using UnityEngine;
using Verse;
using Verse.AI;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
	public class JobGiver_GetNaked : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
            pawn.health.AddHediff(InternalDefOf.VME_Naked);
            pawn.Drawer.renderer.SetAllGraphicsDirty();
            return null;
		}

		
	}
}