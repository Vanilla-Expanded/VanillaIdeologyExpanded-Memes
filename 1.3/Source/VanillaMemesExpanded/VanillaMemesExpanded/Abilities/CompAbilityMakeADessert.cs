using System;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class CompAbilityMakeADessert : CompAbilityEffect
	{
		public new CompProperties_AbilityMakeADessert Props
		{
			get
			{
				return (CompProperties_AbilityMakeADessert)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			IntVec3 cell = target.Cell;

			Thing meal = cell.GetThingList(this.parent.pawn.Map).FirstOrFallback();

            if (meal == null)
            {
				Messages.Message("VME_AbilityNeedsMeal".Translate(), MessageTypeDefOf.RejectInput, true);
				this.parent.StartCooldown(30);
            }
            else
            {
				IntVec3 position = meal.Position;
				Map map = meal.Map;
				int count = meal.stackCount;

				if (meal.def.defName == Props.simpleDessertDef.defName || meal.def.defName == Props.fineDessertDef.defName || meal.def.defName == Props.lavishDessertDef.defName || meal.def.defName == Props.gourmetDessertDef.defName)
				{
					Messages.Message("VME_AbilityAlreadyADessert".Translate(), MessageTypeDefOf.RejectInput, true);
					this.parent.StartCooldown(30);
				}
				else



				if (meal.HasThingCategory(ThingCategoryDefOf.FoodMeals))
				{
					FoodPreferability preferability;





					if (meal.def.ingestible != null)
					{
						preferability = meal.def.ingestible.preferability;

					}
					else
					{
						preferability = FoodPreferability.Undefined;

					}

					if ((preferability == FoodPreferability.MealAwful) || (preferability == FoodPreferability.Undefined) || (preferability == FoodPreferability.MealSimple) || meal.def.defName.Contains("Simple"))
					{

						meal.Destroy();
						ThingDef newThing = Props.simpleDessertDef;
						Thing dessert = GenSpawn.Spawn(newThing, position, map, WipeMode.Vanish);
						dessert.stackCount = count;


					}
					else if ((preferability == FoodPreferability.MealFine) || meal.def.defName.Contains("Fine"))
					{
						meal.Destroy();
						ThingDef newThing = Props.fineDessertDef;
						Thing dessert = GenSpawn.Spawn(newThing, position, map, WipeMode.Vanish);
						dessert.stackCount = count;
					}
					else if ((preferability == FoodPreferability.MealLavish))
					{
						if (meal.def.defName.Contains("Lavish"))
						{
							meal.Destroy();
							ThingDef newThing = Props.lavishDessertDef;
							Thing dessert = GenSpawn.Spawn(newThing, position, map, WipeMode.Vanish);
							dessert.stackCount = count;
						}
						else if (meal.def.defName.Contains("Gourmet"))
						{
							meal.Destroy();
							ThingDef newThing = Props.gourmetDessertDef;
							Thing dessert = GenSpawn.Spawn(newThing, position, map, WipeMode.Vanish);
							dessert.stackCount = count;
						}
					}


				}
				else
				{
					Messages.Message("VME_AbilityNeedsMeal".Translate(), MessageTypeDefOf.RejectInput, true);
					this.parent.StartCooldown(30);

				}


			}


			base.Apply(target, dest);

		}
	}
}
