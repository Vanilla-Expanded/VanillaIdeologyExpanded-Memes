using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaMemesExpanded
{
	public class Need_Desserts : Need
	{
		public override float CurInstantLevel
		{
			get
			{
				if (this.lastDessertUseTick >= Find.TickManager.TicksGame - 10)
				{
					return Mathf.Clamp01(this.lastDessertUsed);
				}
				return 0f;
			}
		}
		public DessertNeedCategory CurCategory
		{
			get
			{
				if (this.CurLevel < 0.1f)
				{
					return DessertNeedCategory.Craving;
				}
				if (this.CurLevel < 0.2f)
				{
					return DessertNeedCategory.Desiring;
				}
				if (this.CurLevel < 0.3f)
				{
					return DessertNeedCategory.Wanting;
				}
				if (this.CurLevel < 0.7f)
				{
					return DessertNeedCategory.RecentlyEaten;
				}
				if (this.CurLevel < 0.9f)
				{
					return DessertNeedCategory.Full;
				}
				return DessertNeedCategory.CompletelyFull;
			}
		}

		public override bool ShowOnNeedList
		{
			get
			{
				
				if(DefDatabase<MemeDef>.GetNamedSilentFail("VME_SweetTeeth")==null || !Current.Game.World.factionManager.OfPlayer.ideos.HasAnyIdeoWithMeme(DefDatabase<MemeDef>.GetNamedSilentFail("VME_SweetTeeth"))|| ExpectationsUtility.CurrentExpectationFor(this.pawn).order <= 2)
                {
					return false;
				}
				else return true;


			}
		}

		

		public override void NeedInterval()
		{
			if (!this.IsFrozen)
			{
                if (ExpectationsUtility.CurrentExpectationFor(this.pawn).order>2)
                {

					this.CurLevel -= this.DessertFallPerTick * 150f;
				}
               
			}
		}

		private float DessertFallPerTick
		{
			get
			{
				return this.def.fallPerDay / 60000f;
			}
		}

		public Need_Desserts(Pawn pawn) : base(pawn)
		{
			
		}

		public void DessertTaken(float dessert)
		{
			this.lastDessertUsed = dessert;
			this.lastDessertUseTick = Find.TickManager.TicksGame;
		}

		public float lastDessertUsed;

		public int lastDessertUseTick;

		

		
	}
}
