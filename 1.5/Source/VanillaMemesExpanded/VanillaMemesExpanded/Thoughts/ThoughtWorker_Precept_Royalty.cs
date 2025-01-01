using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class ThoughtWorker_Precept_Royalty : ThoughtWorker_Precept_Social
    {
        protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
        {

            if (Faction.OfEmpire is null ||  otherPawn.royalty.AllTitlesForReading.Count == 0)
            {
                return false;
            }

            else
            {
                RoyalTitleDef title = otherPawn.royalty.GetCurrentTitleInFaction(Faction.OfEmpire).def;

                float seniority = title.seniority;

                switch (seniority)
                {
                    case 100:
                        return ThoughtState.ActiveAtStage(0);
                    case 200:
                        return ThoughtState.ActiveAtStage(1);
                    case 300:
                        return ThoughtState.ActiveAtStage(2);
                    case 400:
                        return ThoughtState.ActiveAtStage(3);
                    case 500:
                        return ThoughtState.ActiveAtStage(4);
                    case 600:
                        return ThoughtState.ActiveAtStage(5);
                    case 601:
                        return ThoughtState.ActiveAtStage(6);
                    case 602:
                        return ThoughtState.ActiveAtStage(7);
                    case 700:
                        return ThoughtState.ActiveAtStage(8);
                    case 701:
                        return ThoughtState.ActiveAtStage(9);
                    case 800:
                        return ThoughtState.ActiveAtStage(10);
                    case 801:
                        return ThoughtState.ActiveAtStage(11);
                    case 802:
                        return ThoughtState.ActiveAtStage(12);
                    case 900:
                        return ThoughtState.ActiveAtStage(13);
                    case 901:
                        return ThoughtState.ActiveAtStage(14);
                    default:
                        return ThoughtState.ActiveAtStage(0);
                }

                

            }


        }





    }


}

