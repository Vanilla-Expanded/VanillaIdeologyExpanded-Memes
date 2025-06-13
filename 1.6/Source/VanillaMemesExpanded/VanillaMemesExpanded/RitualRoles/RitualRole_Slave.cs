
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class RitualRole_Slave : RitualRole
    {
       

        public override bool AppliesToPawn(Pawn p, out string reason, TargetInfo selectedTarget, LordJob_Ritual ritual = null, RitualRoleAssignments assignments = null, Precept_Ritual precept = null, bool skipReason = false)
        {
            reason = null;
            
            if (!p.IsSlaveOfColony)
            {
                if (!skipReason)
                {
                    reason = "VME_MessageRitualRoleMustBeSlave".Translate(base.LabelCap);
                }
                return false;
            }
           
            return true;
        }

        public override bool AppliesToRole(Precept_Role role, out string reason, Precept_Ritual ritual = null, Pawn p = null, bool skipReason = false)
        {
            reason = null;
            return false;
        }

       
    }
}

