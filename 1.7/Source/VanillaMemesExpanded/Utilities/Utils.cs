using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace VanillaMemesExpanded
{
    public static class Utils
    {

        public static bool TryGetModExtension<T>(this Def def, out T ext) where T : DefModExtension
        {
            ext = def.HasModExtension<T>() ? def.GetModExtension<T>() : null;
            return ext != null;
        }
    }
}
