using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class Thought_Fire_Desired : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {

            if (p.Map?.IsPlayerHome != true)
            {
                return false;
            }


            if (PawnCollectionClass.firesInTheMap == 0)
            {
                return ThoughtState.ActiveAtStage(0);

            }
            else if(PawnCollectionClass.firesInTheMap <5)
            {
                return ThoughtState.ActiveAtStage(1);

            }
            else if (PawnCollectionClass.firesInTheMap <30)
            {
                return ThoughtState.ActiveAtStage(2);

            }
            else if (PawnCollectionClass.firesInTheMap <50)
            {
                return ThoughtState.ActiveAtStage(3);

            }
            else if (PawnCollectionClass.firesInTheMap <150)
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
