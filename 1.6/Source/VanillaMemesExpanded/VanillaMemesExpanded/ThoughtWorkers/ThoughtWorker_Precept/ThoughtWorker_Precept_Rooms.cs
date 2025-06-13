using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class ThoughtWorker_Precept_Rooms : ThoughtWorker_Precept
	{

	
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (p.Map?.IsPlayerHome!=true || !StaticCollections.roomsInMap.ContainsKey(p.Map))
			{
				return false;
			}

			if (StaticCollections.roomsInMap[p.Map] < 5)
            {
				return ThoughtState.ActiveAtStage(4);
			} else if (StaticCollections.roomsInMap[p.Map] < 10)
			{
				return ThoughtState.ActiveAtStage(3);
			}
			else if (StaticCollections.roomsInMap[p.Map] < 15)
			{
				return ThoughtState.ActiveAtStage(2);
			}
			else if (StaticCollections.roomsInMap[p.Map] < 20)
			{
				return ThoughtState.ActiveAtStage(1);
			}
			else 
			{
				return ThoughtState.ActiveAtStage(0);
			}

			
			


		}


	}
}
