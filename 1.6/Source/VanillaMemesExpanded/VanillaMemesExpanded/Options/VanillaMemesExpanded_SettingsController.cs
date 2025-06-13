using RimWorld;
using UnityEngine;
using Verse;


namespace VanillaMemesExpanded
{



    public class VanillaMemesExpanded_Mod : Mod
    {


        public VanillaMemesExpanded_Mod(ModContentPack content) : base(content)
        {
            GetSettings<VanillaMemesExpanded_Settings>();
        }
        public override string SettingsCategory()
        {

            return "Vanilla Ideology Expanded";



        }



        public override void DoSettingsWindowContents(Rect inRect)
        {
            VanillaMemesExpanded_Settings.DoWindowContents(inRect);
        }
    }


}
