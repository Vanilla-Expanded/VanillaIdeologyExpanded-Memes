


using Verse;
using System.Collections.Generic;
using System.Linq;
using RimWorld;

namespace VanillaMemesExpanded
{


    class Thought_DisableFirstDefeatThought : Thought_Memory
    {

        public bool removedOnce = false;


       

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref this.removedOnce, "removedOnce", false, false);
        }

        public override float MoodOffset()
        {
            if (!removedOnce)
            {
               
                this.pawn.needs.mood.thoughts.memories.RemoveMemoriesOfDef(InternalDefOf.VME_Defeat_Dishonorable);
                if (this.pawn.Map.GetComponent<MapComponent_PawnsInMapDesiringRitualSuicide>()?.pawnsDesiringSuicide.Contains(this.pawn)==false)
                {
                    this.pawn.Map.GetComponent<MapComponent_PawnsInMapDesiringRitualSuicide>()?.pawnsDesiringSuicide.Add(this.pawn);
                }

                removedOnce = true;
            }


            return base.MoodOffset();
        }


    }
}
