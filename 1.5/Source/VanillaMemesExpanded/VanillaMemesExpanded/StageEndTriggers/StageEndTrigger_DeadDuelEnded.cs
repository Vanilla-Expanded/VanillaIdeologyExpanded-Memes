using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using Verse.Sound;
using UnityEngine;


namespace VanillaMemesExpanded
{
	public class StageEndTrigger_DeadDuelEnded : StageEndTrigger_AnyPawnDead
	{
		protected override bool Trigger(LordJob_Ritual ritual)
		{
			if (base.Trigger(ritual))
			{
				return true;
			}
			foreach (string roleId in this.roleIds)
			{
				using (IEnumerator<Pawn> enumerator2 = ritual.assignments.AssignedPawns(roleId).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Dead)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}
