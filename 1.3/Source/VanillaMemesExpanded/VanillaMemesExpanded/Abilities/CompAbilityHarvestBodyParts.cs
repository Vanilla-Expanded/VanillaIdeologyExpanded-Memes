using System;
using Verse;
using RimWorld;
using System.Linq;
using Verse.Sound;
namespace VanillaMemesExpanded
{
	public class CompAbilityHarvestBodyParts : CompAbilityEffect
	{
		public new CompProperties_AbilityHarvestBodyParts Props
		{
			get
			{
				return (CompProperties_AbilityHarvestBodyParts)this.props;
			}
		}

		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			IntVec3 cell = target.Cell;

			Corpse corpse = (Corpse)(from x in cell.GetThingList(this.parent.pawn.Map)
								 where (x.GetType()==typeof(Corpse))
								 select x).RandomElement();

			

			if (corpse == null)
			{
				Log.Message("corpse is null");
				Messages.Message("VME_AbilityNeedsCorpse".Translate(), MessageTypeDefOf.RejectInput, true);
				this.parent.StartCooldown(30);
			}
			else
			{
                if (corpse.InnerPawn.RaceProps.Humanlike && !corpse.IsNotFresh())
                {
					ThingDef newThing = Props.bodyParts.RandomElement();
					GenSpawn.Spawn(newThing, cell, this.parent.pawn.Map, WipeMode.Vanish);
					System.Random random = new System.Random();
					double randomChance = random.NextDouble();
					ThingDef secondaryOrgan = null;
					if (randomChance < 0.3)
                    {
						secondaryOrgan = (from x in Props.bodyParts
												   where x != newThing
												   select x).RandomElement();
						GenSpawn.Spawn(secondaryOrgan, cell, this.parent.pawn.Map, WipeMode.Vanish);
					}
					if (randomChance < 0.1)
					{
						ThingDef tertiaryOrgan = (from x in Props.bodyParts
												   where x != newThing && x != secondaryOrgan
												  select x).RandomElement();
						GenSpawn.Spawn(tertiaryOrgan, cell, this.parent.pawn.Map, WipeMode.Vanish);
					}
					for (int i = 0; i < 20; i++)
					{
						IntVec3 c;
						CellFinder.TryFindRandomReachableCellNear(cell, this.parent.pawn.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
						FilthMaker.TryMakeFilth(c, this.parent.pawn.Map, ThingDefOf.Filth_Blood);

					}
					SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(cell, this.parent.pawn.Map, false));
					ThingDef newThingMeat = ThingDefOf.Meat_Human;
					Thing humanMeat = GenSpawn.Spawn(newThingMeat, cell, this.parent.pawn.Map, WipeMode.Vanish);
					humanMeat.stackCount = 60;
					corpse.Destroy();
				}
                else
                {
					Log.Message("corpse is not fresh");
					Messages.Message("VME_AbilityNeedsCorpse".Translate(), MessageTypeDefOf.RejectInput, true);
					this.parent.StartCooldown(30);
				}


			}



			base.Apply(target, dest);

		}
	}
}
