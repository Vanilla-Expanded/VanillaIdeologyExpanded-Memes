using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
    public class RoleRequirement_NoTitles : RoleRequirement
    {


        public override bool Met(Pawn p, Precept_Role role)
        {

            if (p.ideo?.Ideo?.HasPrecept(InternalDefOf.VME_IdeoRole_Majordomo) == true)
            {

                if (p.royalty.AllTitlesForReading.Count != 0)
                {
                    return false;
                }
                return true;


            }
            else return true;




        }
    }

}
