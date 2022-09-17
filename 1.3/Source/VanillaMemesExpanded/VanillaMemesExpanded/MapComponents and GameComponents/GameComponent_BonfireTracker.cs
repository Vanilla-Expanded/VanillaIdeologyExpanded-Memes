using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class GameComponent_BonfireTracker : GameComponent
    {

       
       
        public int tickCounter = 0;
        public int tickInterval = 10000;
        public Dictionary<Pawn, int> colonist_bonfire_tracker = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;



        public GameComponent_BonfireTracker(Game game) : base()
        {

        }

        public override void FinalizeInit()
        {
              
            base.FinalizeInit();

        }

        public override void ExposeData()
       {
           base.ExposeData();

           Scribe_Collections.Look(ref colonist_bonfire_tracker, "colonist_bonfire_tracker", LookMode.Reference, LookMode.Value, ref list2, ref list3);           
          
       }



        public override void GameComponentTick()
        {

          
            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                if(Current.Game.World.factionManager.OfPlayer.ideos.HasAnyIdeoWithMeme(InternalDefOf.VME_FireWorship))
                {
                    foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                    {
                        if (colonist_bonfire_tracker.ContainsKey(pawn))
                        {
                            pawn.needs.joy.GainJoy(0.5f,JoyKindDefOf.Social);
                            colonist_bonfire_tracker[pawn] = colonist_bonfire_tracker[pawn] - tickInterval;
                            if (colonist_bonfire_tracker[pawn] < 0)
                            {
                                RemoveColonist(pawn);
                            }

                        }
                    }

                }

               

                tickCounter = 0;
            }



        }

        public void AddColonistAndTicksToCountdown(Pawn pawn, int ticks)
        {
            if (!colonist_bonfire_tracker.ContainsKey(pawn))
            {
                colonist_bonfire_tracker.Add(pawn, ticks);
            }
            else colonist_bonfire_tracker[pawn] = ticks;
        }

        public void RemoveColonist(Pawn pawn)
        {
            if (colonist_bonfire_tracker.ContainsKey(pawn))
            {
                colonist_bonfire_tracker.Remove(pawn);
            }
        }

       


    }


}
