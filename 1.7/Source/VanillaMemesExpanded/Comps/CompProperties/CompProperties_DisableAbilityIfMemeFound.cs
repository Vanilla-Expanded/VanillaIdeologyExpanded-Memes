using System;
using Verse;
using RimWorld;




namespace VanillaMemesExpanded
{
	public class CompProperties_DisableAbilityIfMemeFound : CompProperties_AbilityEffect
	{
		public CompProperties_DisableAbilityIfMemeFound()
		{
			this.compClass = typeof(CompDisableAbilityIfMemeFound);
		}

		public MemeDef meme;
	}
}

