using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using RimWorld.Planet;


namespace VanillaMemesExpanded
{
    public class WorldComponent_AgeAndIllnessTracker : WorldComponent
    {

       
       
        public int tickCounter = tickInterval;
        public const int tickInterval = 3000;
        public Dictionary<Pawn, int> colonist_illness_tracker = new Dictionary<Pawn, int>();
        List<Pawn> list2;
        List<int> list3;


        public static WorldComponent_AgeAndIllnessTracker Instance;

        public WorldComponent_AgeAndIllnessTracker(World world) : base(world) => Instance = this;
   


        public override void ExposeData()
       {
           base.ExposeData();

           Scribe_Collections.Look(ref colonist_illness_tracker, "colonist_illness_tracker", LookMode.Reference, LookMode.Value, ref list2, ref list3);           
          
       }


        public void AddColonistToIllnessList(Pawn pawn, int ticks)
        {
            if (pawn != null && !colonist_illness_tracker.ContainsKey(pawn))
            {
                colonist_illness_tracker[pawn] = ticks;
            }
        }

        public  void IncreasePawnIllnessTicks(Pawn pawn, int ticks)
        {
            if (pawn != null)
            {
                colonist_illness_tracker[pawn] += ticks;
            }
        }
        public  void ResetPawnIllnessTicks(Pawn pawn)
        {
            if (pawn != null)
            {
                colonist_illness_tracker[pawn] = 0;
            }
        }


        public override void WorldComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Illness_Exalted) != null) 
                {
                    List<Pawn> listPawns = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists;
                    foreach(Pawn p in listPawns)
                    {
                        if (p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Illness_Exalted) ==true)
                        {
                            AddColonistToIllnessList(p,0);

                            if (p.health.hediffSet.AnyHediffMakesSickThought)
                            {
                                ResetPawnIllnessTicks(p);
                            }
                            else { 
                                if(colonist_illness_tracker[p]<int.MaxValue- tickInterval)
                                {
                                    IncreasePawnIllnessTicks(p, tickInterval);

                                }
                            }


                        }

                    }

                }

                if (Current.Game.World.factionManager.OfPlayer.ideos.GetPrecept(InternalDefOf.VME_Elders_Holy) != null)
                {
                    List<Pawn> listPawns = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists;
                    foreach (Pawn p in listPawns)
                    {
                        if (p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Elders_Holy)==true)
                        {

                            if (p.ageTracker.AgeBiologicalYears > 65 && !p.story.traits.HasTrait(InternalDefOf.VME_Elder))
                            {
                                Trait trait = new Trait(InternalDefOf.VME_Elder, 0, true);
                                p.story.traits.GainTrait(trait);
                            }

                        }
                        if (p.ageTracker.AgeBiologicalYears < 65 && p.story.traits.HasTrait(InternalDefOf.VME_Elder))
                        {
                            p.story.traits.RemoveTrait(p.story.traits.GetTrait(InternalDefOf.VME_Elder));
                        }

                    }
                        

                }

                tickCounter = 0;
            }



        }


    }


}
