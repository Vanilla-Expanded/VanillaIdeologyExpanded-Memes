using System;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;


namespace VanillaMemesExpanded
{
    public class MapComponent_PawnsInMapDesiringRitualSuicide : MapComponent
    {



        public List<Pawn> pawnsDesiringSuicide = new List<Pawn>();


        public MapComponent_PawnsInMapDesiringRitualSuicide(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref pawnsDesiringSuicide, "pawnsDesiringSuicide", LookMode.Reference);

            base.ExposeData();
        }





    }


}
