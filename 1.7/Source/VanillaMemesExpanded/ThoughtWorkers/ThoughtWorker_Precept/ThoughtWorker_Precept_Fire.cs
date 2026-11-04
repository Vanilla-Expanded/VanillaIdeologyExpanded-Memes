using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class ThoughtWorker_Precept_Fire : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {

            if (p.Map?.IsPlayerHome != true || !StaticCollections.firesInTheMap.ContainsKey(p.Map))
            {
                return false;
            }


            if (StaticCollections.firesInTheMap[p.Map] == 0)
            {
                return ThoughtState.ActiveAtStage(0);

            }
            else if(StaticCollections.firesInTheMap[p.Map] <5)
            {
                return ThoughtState.ActiveAtStage(1);

            }
            else if (StaticCollections.firesInTheMap[p.Map] < 30)
            {
                return ThoughtState.ActiveAtStage(2);

            }
            else if (StaticCollections.firesInTheMap[p.Map] < 50)
            {
                return ThoughtState.ActiveAtStage(3);

            }
            else if (StaticCollections.firesInTheMap[p.Map] < 150)
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
