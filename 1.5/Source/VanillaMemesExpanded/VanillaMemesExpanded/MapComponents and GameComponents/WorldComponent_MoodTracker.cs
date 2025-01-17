using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;

namespace VanillaMemesExpanded
{
    public class WorldComponent_MoodTracker : WorldComponent
    {

        public int tickCounter = tickInterval;
        public const int tickInterval = 6000;
        public float averageColonyMood;
        public Dictionary<Pawn, int> colonist_and_random_mood = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;

        public static WorldComponent_MoodTracker Instance;

        public WorldComponent_MoodTracker(World world) : base(world) => Instance = this;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref colonist_and_random_mood, "colonist_and_random_mood", LookMode.Reference, LookMode.Value, ref list2, ref list3);
            Scribe_Values.Look<float>(ref this.averageColonyMood, "averageColonyMood");
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterMood", 0, true);
        }
        public override void WorldComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (Current.Game.World.factionManager.OfPlayer.ideos.HasAnyIdeoWithMeme(InternalDefOf.VME_Gestalt))
                {
                    List<Pawn> colonistList = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists;
                    float totalMood = 0;
                    foreach (Pawn pawn in colonistList)
                    {
                        if (pawn?.needs?.mood?.thoughts != null)
                        {
                            totalMood += pawn.needs.mood.thoughts.TotalMoodOffset();
                        }

                    }

                    averageColonyMood = totalMood;
                }

                tickCounter = 0;
            }



        }

        public void AddColonistAndRandomMood(Pawn pawn, int mood)
        {
            if (pawn != null)
            {

                colonist_and_random_mood[pawn] = mood;
            }


        }


    }


}



