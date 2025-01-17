﻿using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class Thought_StageByTime : ThoughtWorker_Precept
	{

		public int gracePeriod = 900000; //15 days
		public int firstPeriod = 960000; //16 days
		public int secondPeriod = 720000; //17 days
		public int thirdPeriod = 1200000; //20 days
		public int twoquadrumsPeriod = 1800000; //30 days
		public int fortydaysPeriod = 2400000; //40 days
		public int fiftydaysPeriod = 3000000; //50 days
		public int maxPeriod = 3600000; //60 days

		protected override ThoughtState ShouldHaveThought(Pawn p)
		{


			
			if (StaticCollections.ticksWithoutAbandoning < gracePeriod)
            {
				return false;

			} else if (StaticCollections.ticksWithoutAbandoning < firstPeriod)
			{
				return ThoughtState.ActiveAtStage(0);


			}
			else if (StaticCollections.ticksWithoutAbandoning < secondPeriod)
			{
				return ThoughtState.ActiveAtStage(1);


			}
			else if (StaticCollections.ticksWithoutAbandoning < thirdPeriod)
			{
				return ThoughtState.ActiveAtStage(2);


			}
			else if (StaticCollections.ticksWithoutAbandoning < twoquadrumsPeriod)
			{
				return ThoughtState.ActiveAtStage(3);


			}
			else if (StaticCollections.ticksWithoutAbandoning < fortydaysPeriod)
			{
				return ThoughtState.ActiveAtStage(4);


			}
			else if (StaticCollections.ticksWithoutAbandoning < fiftydaysPeriod)
			{
				return ThoughtState.ActiveAtStage(5);


			}
			else if (StaticCollections.ticksWithoutAbandoning < maxPeriod)
			{
				return ThoughtState.ActiveAtStage(6);


			}
			else
            {
				return ThoughtState.ActiveAtStage(7);
			}

			
			



		}


	}
}
