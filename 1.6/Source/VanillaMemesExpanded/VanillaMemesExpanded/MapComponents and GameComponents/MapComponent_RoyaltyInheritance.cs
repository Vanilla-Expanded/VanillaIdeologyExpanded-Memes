using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace VanillaMemesExpanded
{
    public class MapComponent_RoyaltyInheritance : MapComponent
    {



        public int tickCounter = tickInterval;
        public const int tickInterval = 6000;


        public MapComponent_RoyaltyInheritance(Map map) : base(map)
        {

        }



        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterRoyalty", 0, true);


        }

        public Pawn CheckFirstBorn(Pawn pawn)
        {
            List<Pawn> children = pawn.relations.Children.ToList();
            if (children.Count > 0)
            {
                Pawn heir = null;
                List <Pawn> livingChildren = (from x in children
                                             where !x.Dead
                                             orderby x.ageTracker.AgeBiologicalTicks
                                             select x).ToList();

                if (!livingChildren.NullOrEmpty())
                {
                    heir = livingChildren.Last();
                }
                
                return heir;
            }

            return null;
        }

        public override void MapComponentTick()
        {
            if (Find.IdeoManager.classicMode) return;

            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                if (Current.Game.World.factionManager.OfPlayer.ideos?.GetPrecept(InternalDefOf.VME_TitleInheritance_Inherited) != null)

                {
                   
                    foreach (Pawn pawn in map.mapPawns.FreeColonistsSpawned)
                    {
                      
                        if (pawn.royalty.AllTitlesForReading.Count > 0) {
                           
                            if (pawn.Ideo?.GetPrecept(InternalDefOf.VME_TitleInheritance_Inherited) != null)
                            {
                                Pawn heir = CheckFirstBorn(pawn);
                            
                                if(heir != null)
                                {
                                    foreach (RoyalTitle title in pawn.royalty.AllTitlesForReading)
                                    {
                                        if (pawn.royalty.GetHeir(title.faction) != heir)
                                        {
                                            pawn.royalty.SetHeir(heir, title.faction);
                                            Find.LetterStack.ReceiveLetter("VME_HeirChange".Translate(pawn.NameFullColored, heir.NameFullColored), "VME_HeirChangeDesc".Translate(pawn.NameFullColored, heir.NameFullColored), LetterDefOf.NeutralEvent, heir, null, null, null, null);

                                        }

                                    }

                                }
                       

                            }

                        }

                        
                    }
                }



                tickCounter = 0;
            }








        }




    }


}



