
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{

    public class ThingSetMaker_StartingItemsEldritchCult : ThingSetMaker
    {
        protected override void Generate(ThingSetMakerParams parms, List<Thing> outThings)
        {
            ThingDef stuff = GenStuff.RandomStuffByCommonalityFor(InternalDefOf.VME_Obelisk, TechLevel.Undefined);
            Thing thing = ThingMaker.MakeThing(InternalDefOf.VME_Obelisk, stuff);
            Thing thing2 = ThingMaker.MakeThing(InternalDefOf.VME_Obelisk, stuff);
            Thing thing3 = ThingMaker.MakeThing(InternalDefOf.VME_Obelisk, stuff);

            outThings.Add(thing);
            outThings.Add(thing2);
            outThings.Add(thing3);

        }

        protected override IEnumerable<ThingDef> AllGeneratableThingsDebugSub(ThingSetMakerParams parms)
        {
            throw new NotImplementedException();
        }



    }
}

