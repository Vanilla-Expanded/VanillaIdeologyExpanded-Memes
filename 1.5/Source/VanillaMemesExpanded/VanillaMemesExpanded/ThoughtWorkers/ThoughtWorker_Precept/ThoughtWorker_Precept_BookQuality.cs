using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class ThoughtWorker_Precept_BookQuality : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {

            if (p.Map?.IsPlayerHome != true || !StaticCollections.legendaryBooksInTheMap.ContainsKey(p.Map))
            {
                return false;
            }

            if (StaticCollections.legendaryBooksInTheMap[p.Map] == 0)
            {
                return false;

            }
            else
            if (StaticCollections.legendaryBooksInTheMap[p.Map] == 1)
            {
                return ThoughtState.ActiveAtStage(0);

            }
            else if(StaticCollections.legendaryBooksInTheMap[p.Map] ==2)
            {
                return ThoughtState.ActiveAtStage(1);

            }
            else if (StaticCollections.legendaryBooksInTheMap[p.Map] ==3)
            {
                return ThoughtState.ActiveAtStage(2);

            }
            else if (StaticCollections.legendaryBooksInTheMap[p.Map] ==4)
            {
                return ThoughtState.ActiveAtStage(3);

            }
            else if (StaticCollections.legendaryBooksInTheMap[p.Map] >= 5)
            {
                return ThoughtState.ActiveAtStage(4);

            }
            return false;












        }


    }
}
