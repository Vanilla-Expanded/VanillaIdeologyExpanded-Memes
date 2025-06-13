using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class ThoughtWorker_Precept_Royalty_AnyTitle : ThoughtWorker_Precept_Social
    {
        protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
        {

            if (otherPawn.royalty.AllTitlesForReading.Count == 0)
            {
                return false;
            }

            else
            {

                return ThoughtState.ActiveAtStage(0);




            }


        }





    }


}

