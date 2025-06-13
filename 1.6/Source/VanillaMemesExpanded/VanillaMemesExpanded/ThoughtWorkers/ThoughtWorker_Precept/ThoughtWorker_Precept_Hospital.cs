using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Hospital : ThoughtWorker_Precept
	{


		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (p.Map?.IsPlayerHome != true || !StaticCollections.hospitalTilesInMap.ContainsKey(p.Map))
			{
				return false;
			}
            if (StaticCollections.hospitalTilesInMap[p.Map] == 0)
            {
                return ThoughtState.ActiveAtStage(0);

            }else
            if (!StaticCollections.hospitalImpressive[p.Map])
            {
                return ThoughtState.ActiveAtStage(1);
            }
            
            else if (StaticCollections.hospitalDirty[p.Map])
			{
				return ThoughtState.ActiveAtStage(2);
			}
			else if (StaticCollections.hospitalTilesInMap[p.Map] < 25)
			{
				return ThoughtState.ActiveAtStage(3);
			}
			else if (StaticCollections.hospitalTilesInMap[p.Map] < 50)
			{
				return ThoughtState.ActiveAtStage(4);
			}
			
			else
			{
				return ThoughtState.ActiveAtStage(5);
			}





		}


	}
}
