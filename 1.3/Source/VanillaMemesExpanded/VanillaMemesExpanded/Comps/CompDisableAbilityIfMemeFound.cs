using System;
using Verse;
using RimWorld;




namespace VanillaMemesExpanded
{
	public class CompDisableAbilityIfMemeFound : CompAbilityEffect
	{
		public new CompProperties_DisableAbilityIfMemeFound Props
		{
			get
			{
				return (CompProperties_DisableAbilityIfMemeFound)this.props;
			}
		}

		public override bool ShouldHideGizmo
		{
			get
			{
                if (this.parent.pawn.Ideo.HasMeme(Props.meme))
                {
					return true;
                }else
				return false;
			}
		}
	}
}