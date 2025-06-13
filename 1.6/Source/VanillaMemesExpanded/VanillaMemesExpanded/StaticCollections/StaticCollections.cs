
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;


namespace VanillaMemesExpanded
{

    public static class StaticCollections
    {

        //This static class stores different lists as a cache





        public static Dictionary<Pawn, int> colonist_junk_tracker = new Dictionary<Pawn, int>();

        public static Dictionary<Pawn, bool> colonist_obelisk_tracker = new Dictionary<Pawn, bool>();

        public static Dictionary<Map, int> hospitalTilesInMap = new Dictionary<Map, int>();

        public static Dictionary<Map, bool> hospitalDirty = new Dictionary<Map, bool>();

        public static Dictionary<Map, bool> hospitalImpressive = new Dictionary<Map, bool>();

        public static Dictionary<Map, int> libraryTilesInMap = new Dictionary<Map, int>();

        public static Dictionary<Map, bool> libraryDirty = new Dictionary<Map, bool>();

        public static Dictionary<Map, bool> libraryImpressive = new Dictionary<Map, bool>();

        public static Dictionary<Map, int> roomsInMap = new Dictionary<Map, int>();

        public static HashSet<Thing> objectsToDeconstruct_InMap = new HashSet<Thing>();

        public static Dictionary<Map, int> firesInTheMap = new Dictionary<Map, int>();

        public static Dictionary<Map, int> pensInTheMap = new Dictionary<Map, int>();

        public static Dictionary<Map, int> legendaryBooksInTheMap = new Dictionary<Map, int>();

        public static HashSet<string> drinksForAlcoholPrecepts = new HashSet<string>();

        public static HashSet<string> fireSourcesForFirePrecepts = new HashSet<string>();

        public static HashSet<string> untameableInsectsForInsectoidPrecepts = new HashSet<string>();

        public static HashSet<string> naturalImplants = new HashSet<string>();



        static StaticCollections()
        {

            HashSet<SupportedDrinksForPreceptDefs> allAlcoholLists = DefDatabase<SupportedDrinksForPreceptDefs>.AllDefsListForReading.ToHashSet();
            foreach (SupportedDrinksForPreceptDefs individualList in allAlcoholLists)
            {
                drinksForAlcoholPrecepts.AddRange(individualList.supportedDrinksForPrecept);
            }
            HashSet<FireSourcesForPreceptDefs> allFireLists = DefDatabase<FireSourcesForPreceptDefs>.AllDefsListForReading.ToHashSet();
            foreach (FireSourcesForPreceptDefs individualList in allFireLists)
            {
                fireSourcesForFirePrecepts.AddRange(individualList.supportedFireSourcesForPrecept);
            }
            HashSet<UntameableInsectDefs> allInsectLists = DefDatabase<UntameableInsectDefs>.AllDefsListForReading.ToHashSet();
            foreach (UntameableInsectDefs individualList in allInsectLists)
            {
                untameableInsectsForInsectoidPrecepts.AddRange(individualList.supportedUntameableInsectsForPrecept);
            }
            HashSet<NaturalImplantCategoryDefs> allImplantsLists = DefDatabase<NaturalImplantCategoryDefs>.AllDefsListForReading.ToHashSet();
            foreach (NaturalImplantCategoryDefs individualList in allImplantsLists)
            {
                naturalImplants.AddRange(individualList.supportedNaturalImplantThingDefs);
                foreach (string category in individualList.supportedNaturalImplantCategories)
                {
                    ThingCategoryDef categoryDef = DefDatabase<ThingCategoryDef>.GetNamedSilentFail(category);
                    if (categoryDef != null)
                    {
                        List<ThingDef> thingsInCategory = categoryDef.childThingDefs;
                        naturalImplants.AddRange(thingsInCategory.Select(x => x.defName));
                    }

                }

            }


        }

        public static void AddColonistToJunkList(Pawn pawn, int numOfJunk)
        {
            if (pawn != null)
            {
                colonist_junk_tracker[pawn] = numOfJunk;
            }
        }

        public static void SetPawnJunk(Pawn pawn, int numOfJunk)
        {
            if (pawn != null)
            {
                colonist_junk_tracker[pawn] = numOfJunk;
            }
        }


        public static void AddDeconstructibleObjectToMap(Thing thing)
        {
            bool flag = !objectsToDeconstruct_InMap.Contains(thing);
            if (flag)
            {
                objectsToDeconstruct_InMap.Add(thing);
            }
        }

        public static void RemoveDeconstructibleObjectFromMap(Thing thing)
        {
            bool flag = objectsToDeconstruct_InMap.Contains(thing);
            if (flag)
            {
                objectsToDeconstruct_InMap.Remove(thing);
            }
        }

        public static void AddColonistAndObelisk(Pawn pawn, bool obeliskPresent)
        {
            if (pawn != null)
            {

                colonist_obelisk_tracker[pawn] = obeliskPresent;
            }

        }



        public static void SetHospitalTilesInMap(Map map, int tiles)
        {
            hospitalTilesInMap[map] = tiles;
        }
        public static void SetHospitalCleanlinessInMap(Map map, bool clean)
        {
            hospitalDirty[map] = clean;
        }
        public static void SetHospitalImpressiveInMap(Map map, bool impressive)
        {
            hospitalImpressive[map] = impressive;
        }
        public static void SetLibraryTilesInMap(Map map, int tiles)
        {
            libraryTilesInMap[map] = tiles;
        }
        public static void SetLibraryCleanlinessInMap(Map map, bool clean)
        {
            libraryDirty[map] = clean;
        }
        public static void SetLibraryImpressiveInMap(Map map, bool impressive)
        {
            libraryImpressive[map] = impressive;
        }
        public static void SetRoomInMap(Map map, int rooms)
        {
            roomsInMap[map] = rooms;
        }
        public static void SetFireInMap(Map map, int fires)
        {
            firesInTheMap[map] = fires;
        }
        public static void SetPensInMap(Map map, int pens)
        {
            pensInTheMap[map] = pens;
        }

        public static void SetLegendaryBooksInMap(Map map, int books)
        {
            legendaryBooksInTheMap[map] = books;
        }
    }
}
