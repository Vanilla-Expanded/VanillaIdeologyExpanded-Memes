using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace VanillaMemesExpanded
{
    public class GameComponent_MoodTracker : GameComponent
    {



        public int tickCounter = 0;
        public int tickInterval = 6000;
        public float averageColonyMood_backup;
        public Dictionary<Pawn, int> colonist_and_random_mood_backup = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;


        public GameComponent_MoodTracker(Game game) : base()
        {

        }

        public override void FinalizeInit()
        {
            
            PawnCollectionClass.averageColonyMood = averageColonyMood_backup;
            PawnCollectionClass.colonist_and_random_mood = colonist_and_random_mood_backup;

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref colonist_and_random_mood_backup, "colonist_and_random_mood_backup", LookMode.Reference, LookMode.Value, ref list2, ref list3);
            Scribe_Values.Look<float>(ref this.averageColonyMood_backup, "averageColonyMood_backup");
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterMood", 0, true);

        }
        public override void GameComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                colonist_and_random_mood_backup = PawnCollectionClass.colonist_and_random_mood;


                if (Current.Game.World.factionManager.OfPlayer.ideos.HasAnyIdeoWithMeme(InternalDefOf.VME_Gestalt))
                {
                    List<Pawn> colonistList = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists;
                    float totalMood = 0;
                    foreach (Pawn pawn in colonistList)
                    {
                        if(pawn?.needs?.mood?.thoughts != null) {
                            totalMood += pawn.needs.mood.thoughts.TotalMoodOffset();
                        }
                       
                    }
                   
                    averageColonyMood_backup = totalMood;
                    PawnCollectionClass.averageColonyMood = totalMood;
                }

                tickCounter = 0;
            }



        }
       



    }


}



