﻿using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class ThoughtWorker_Precept_Ranching_Disliked : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {

            if (p.Map?.IsPlayerHome != true)
            {
                return false;
            }

            if (StaticCollections.pensInTheMap == 0)
            {
                return ThoughtState.ActiveAtStage(0);

            }
            else if (StaticCollections.pensInTheMap < 2)
            {
                return ThoughtState.ActiveAtStage(1);

            }
            else if (StaticCollections.pensInTheMap < 4)
            {
                return ThoughtState.ActiveAtStage(2);

            }
            else 
            {
                return ThoughtState.ActiveAtStage(3);

            }
           












        }


    }
}
