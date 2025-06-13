using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class ThoughtWorker_Precept_Library : ThoughtWorker_Precept
    {


        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (p.Map?.IsPlayerHome != true || !StaticCollections.libraryTilesInMap.ContainsKey(p.Map))
            {
                return false;
            }
            if (StaticCollections.libraryTilesInMap[p.Map] == 0)
            {
                return ThoughtState.ActiveAtStage(0);

            }else
            if (!StaticCollections.libraryImpressive[p.Map])
            {
                return ThoughtState.ActiveAtStage(1);
            }
            
            
            else if (StaticCollections.libraryDirty[p.Map])
            {
                return ThoughtState.ActiveAtStage(2);
            }
            else if (StaticCollections.libraryTilesInMap[p.Map] < 50)
            {
                return ThoughtState.ActiveAtStage(3);
            }
            else if (StaticCollections.libraryTilesInMap[p.Map] < 100)
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
