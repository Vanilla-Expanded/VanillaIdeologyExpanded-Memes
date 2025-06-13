using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class RoleRequirement_HighestTitle : RoleRequirement
    {


        public override bool Met(Pawn p, Precept_Role role)
        {

            if (p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_Leader_HighestTitle) == true)
            {

                if (WorldComponent_BestPsycasterAndTitleTracker.Instance.currentTitleLeaderPawn == p)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            else return true;

        }
    }

}
