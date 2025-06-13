﻿// RimWorld.RitualBehaviorWorker_Conversion
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using UnityEngine;


namespace VanillaMemesExpanded
{

    public class RitualBehaviorWorker_LeaderConversion : RitualBehaviorWorker
    {
        public RitualBehaviorWorker_LeaderConversion()
        {
        }

        public RitualBehaviorWorker_LeaderConversion(RitualBehaviorDef def)
            : base(def)
        {
        }

        public override string CanStartRitualNow(TargetInfo target, Precept_Ritual ritual, Pawn selectedPawn = null, Dictionary<string, Pawn> forcedForRole = null)
        {
            Precept_Role precept_Role = ritual.ideo.RolesListForReading.FirstOrDefault((Precept_Role r) => r.def == PreceptDefOf.IdeoRole_Leader);
            if (precept_Role != null)
            {
                if (precept_Role.ChosenPawnSingle() == null)
                {
                    return "CantStartRitualRoleNotAssigned".Translate(precept_Role.LabelCap);
                }
                bool flag = false;
                foreach (Pawn item in target.Map.mapPawns.FreeColonistsAndPrisonersSpawned)
                {
                    if (ValidateConvertee(item, precept_Role.ChosenPawnSingle(), throwMessages: false))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    return "CantStartRitualNoConvertee".Translate(precept_Role.ChosenPawnSingle().Ideo.name);
                }
                if (!precept_Role.ChosenPawnSingle().Ideo.HasMeme(InternalDefOf.VME_ExaltedPriesthood))
                {
                    return "VME_NeedsExaltedPriesthood".Translate(precept_Role.ChosenPawnSingle().Ideo.name);
                }
            }
            


            return base.CanStartRitualNow(target, ritual, selectedPawn, forcedForRole);
        }

        public static bool ValidateConvertee(Pawn convertee, Pawn leader, bool throwMessages)
        {
            if (convertee == null)
            {
                return false;
            }
            if (!AbilityUtility.ValidateNoMentalState(convertee, throwMessages,null))
            {
                return false;
            }
            if (!AbilityUtility.ValidateCanWalk(convertee, throwMessages, null))
            {
                return false;
            }
            if (!AbilityUtility.ValidateNotSameIdeo(leader, convertee, throwMessages, null))
            {
                return false;
            }
            return true;
        }

        public override void PostCleanup(LordJob_Ritual ritual)
        {
            Pawn warden = ritual.PawnWithRole("leader");
            Pawn pawn = ritual.PawnWithRole("convertee");
            if (pawn.IsPrisonerOfColony)
            {
                WorkGiver_Warden_TakeToBed.TryTakePrisonerToBed(pawn, warden);
                pawn.guest.WaitInsteadOfEscapingFor(1250);
            }
        }
    }
}
