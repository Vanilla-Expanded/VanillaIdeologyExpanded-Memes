using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class Thought_MoodByRooms : ThoughtWorker_Precept
	{

	
		protected override ThoughtState ShouldHaveThought(Pawn p)
		{
			if (p.Map?.IsPlayerHome!=true || !PawnCollectionClass.roomsInMap.ContainsKey(p.Map))
			{
				return false;
			}

			if (PawnCollectionClass.roomsInMap[p.Map] < 5)
            {
				return ThoughtState.ActiveAtStage(4);
			} else if (PawnCollectionClass.roomsInMap[p.Map] < 10)
			{
				return ThoughtState.ActiveAtStage(3);
			}
			else if (PawnCollectionClass.roomsInMap[p.Map] < 15)
			{
				return ThoughtState.ActiveAtStage(2);
			}
			else if (PawnCollectionClass.roomsInMap[p.Map] < 20)
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
