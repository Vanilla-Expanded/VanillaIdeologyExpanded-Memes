using RimWorld;
using Verse;

namespace VanillaMemesExpanded
{
    public interface IVictimPreceptComp
    {
        bool VictimWillingToDo(HistoryEvent ev);
    }

    public class PreceptComp_UnwillingToBeDone : PreceptComp, IVictimPreceptComp
    {
        public HistoryEventDef eventDef;

        public virtual bool VictimWillingToDo(HistoryEvent ev)
        {
            return eventDef == null || eventDef != ev.def;
        }
    }

    public class RequirePrecept : DefModExtension
    {
        public PreceptDef precept;
    }

    public class RelatedInteractionMode : DefModExtension
    {
        public PrisonerInteractionModeDef related;
    }
}