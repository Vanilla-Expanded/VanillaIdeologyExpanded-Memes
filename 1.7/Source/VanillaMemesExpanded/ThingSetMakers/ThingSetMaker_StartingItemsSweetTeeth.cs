
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{

    public class ThingSetMaker_StartingItemsSweetTeeth : ThingSetMaker
    {
        protected override void Generate(ThingSetMakerParams parms, List<Thing> outThings)
        {
          
            Thing thing = ThingMaker.MakeThing(ThingDefOf.Chocolate);
            thing.stackCount = 15;
            outThings.Add(thing);
           
        }

        protected override IEnumerable<ThingDef> AllGeneratableThingsDebugSub(ThingSetMakerParams parms)
        {
            throw new NotImplementedException();
        }



    }
}

