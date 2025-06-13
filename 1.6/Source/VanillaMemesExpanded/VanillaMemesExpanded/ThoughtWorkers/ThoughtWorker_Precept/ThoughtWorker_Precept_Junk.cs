using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class ThoughtWorker_Precept_Junk : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (p.Map?.IsPlayerHome != true)
            {
                return false;
            }

            if (!StaticCollections.colonist_junk_tracker.ContainsKey(p))
            {
                return false;
            }
            else if (StaticCollections.colonist_junk_tracker[p] == 0)
            {
                return ThoughtState.ActiveAtStage(0);

            }
            else
            {
                if (StaticCollections.colonist_junk_tracker[p] ==1)
                { return ThoughtState.ActiveAtStage(1); }
                else
                if (StaticCollections.colonist_junk_tracker[p] == 2)
                { return ThoughtState.ActiveAtStage(2); }
                else
                if (StaticCollections.colonist_junk_tracker[p] == 3)
                { return ThoughtState.ActiveAtStage(3); }
                else
                if (StaticCollections.colonist_junk_tracker[p] == 4)
                { return ThoughtState.ActiveAtStage(4); }
                else
                if (StaticCollections.colonist_junk_tracker[p] == 5)
                { return ThoughtState.ActiveAtStage(5); }
                else
                if (StaticCollections.colonist_junk_tracker[p] == 6)
                { return ThoughtState.ActiveAtStage(6); }
                else
                if (StaticCollections.colonist_junk_tracker[p] < 8)
                { return ThoughtState.ActiveAtStage(7); }
                else
                if (StaticCollections.colonist_junk_tracker[p] < 10)
                { return ThoughtState.ActiveAtStage(8); }
                else
                return ThoughtState.ActiveAtStage(9);






            }



        }


    }
}
