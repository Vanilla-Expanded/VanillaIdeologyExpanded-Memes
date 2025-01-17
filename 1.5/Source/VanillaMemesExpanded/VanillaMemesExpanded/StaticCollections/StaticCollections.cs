
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

        public static Dictionary<Map, int> roomsInMap = new Dictionary<Map, int>();

        public static HashSet<Thing> objectsToDeconstruct_InMap = new HashSet<Thing>();

        public static Dictionary<Map, int> firesInTheMap = new Dictionary<Map, int>();

        public static Dictionary<Map, int> pensInTheMap = new Dictionary<Map, int>();


   

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
            if (pawn != null) {

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
    }
}
