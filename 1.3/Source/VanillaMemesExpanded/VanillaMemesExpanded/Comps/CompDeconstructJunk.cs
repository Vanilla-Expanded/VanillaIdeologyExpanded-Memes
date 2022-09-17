using System;
using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;

namespace VanillaMemesExpanded
{
	
	public class CompDeconstructJunk : ThingComp
	{
		public CompProperties_DeconstructJunk Props
		{
			get
			{
				return (CompProperties_DeconstructJunk)this.props;
			}
		}

		private void SetObjectForDeconstruction()
		{
			this.itemNeedsDeconstruction = true;
			PawnCollectionClass.AddDeconstructibleObjectToMap(this.parent);
		}

		private void CancelObjectForDeconstruction()
		{
			this.itemNeedsDeconstruction = false;
			PawnCollectionClass.RemoveDeconstructibleObjectFromMap(this.parent);

		}

		public override void PostDeSpawn(Map map)
		{			
			if (map != null)
			{
				PawnCollectionClass.RemoveDeconstructibleObjectFromMap(this.parent);
			}
		}

		public override void PostDestroy(DestroyMode mode, Map previousMap)
		{
			if (previousMap != null)
			{
				PawnCollectionClass.RemoveDeconstructibleObjectFromMap(this.parent);
			}
		}

		public bool itemNeedsDeconstruction = false;

		

		public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
			if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Junk_Beautiful) != null|| Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Junk_Preferred) != null)
            {
				bool flag = this.itemNeedsDeconstruction;
				if (flag)
				{
					yield return new Command_Action
					{
						action = new Action(this.CancelObjectForDeconstruction),
						hotKey = KeyBindingDefOf.Misc2,
						defaultDesc = this.Props.buttonCancelDesc.Translate(),
						icon = ContentFinder<Texture2D>.Get(this.Props.buttonCancelIcon, true),
						defaultLabel = this.Props.buttonCancelLabel.Translate()
					};
				}
				else
				{
					yield return new Command_Action
					{
						action = new Action(this.SetObjectForDeconstruction),
						hotKey = KeyBindingDefOf.Misc2,
						defaultDesc = this.Props.buttonDesc.Translate(),
						icon = ContentFinder<Texture2D>.Get(this.Props.buttonIcon, true),
						defaultLabel = this.Props.buttonLabel.Translate()
					};
				}
				yield break;
			}

        }
    }
}
