using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.QuestGen;
using UnityEngine;
using Verse;

namespace VanillaMemesExpanded
{
    public class InteractionWorker_Interrogate : InteractionWorker
    {
        public override void Interacted(Pawn initiator, Pawn recipient, List<RulePackDef> extraSentencePacks, out string letterText, out string letterLabel,
            out LetterDef letterDef, out LookTargets lookTargets)
        {
            var chance = SuccessChance(initiator, recipient);
            if (Rand.Chance(chance))
            {
                extraSentencePacks.Add(InternalDefOf.VFEA_InterrogationSuccess);
                var script = DefDatabase<QuestScriptDef>.AllDefs.Where(def => def.defName.Contains("Opportunity") && def.IsRootRandomSelected).RandomElement();
                var quest = QuestGen.Generate(script, new Slate());
                quest.SetInitiallyAccepted();
                Find.QuestManager.Add(quest);
                var worldObject = quest.PartsListForReading.OfType<QuestPart_SpawnWorldObject>().First().worldObject;
                letterLabel = "VFEAncients.InterrogationSuccess".Translate();
                letterText = "VFEAncients.InterrogationSuccessDetail".Translate(initiator.LabelShortCap, recipient.LabelShortCap, worldObject.LabelShort);
                letterDef = LetterDefOf.PositiveEvent;
                lookTargets = worldObject;
                QuestUtility.SendLetterQuestAvailable(quest);
            }
            else
            {
                var taggedString2 = "VFEAncients.InterrogationFailure".Translate(chance.ToStringPercent());
                MoteMaker.ThrowText((initiator.DrawPos + recipient.DrawPos) / 2f, initiator.Map, taggedString2, 8f);
                extraSentencePacks.Add(InternalDefOf.VFEA_InterrogationRefused);
                letterLabel = null;
                letterDef = null;
                letterText = null;
                lookTargets = null;
            }
        }

        public float SuccessChance(Pawn initiator, Pawn recipient)
        {
            var fromSkill = Mathf.InverseLerp(0f, 25f, initiator.skills.GetSkill(SkillDefOf.Social).Level + 1f);
            var fromPain = Mathf.LerpUnclamped(0.25f, 1.5f, recipient.health.hediffSet.PainTotal);
            Log.Message($"SuccessChance={fromSkill * fromPain},fromSkill={fromSkill},fromPain={fromPain}");
            return fromSkill * fromPain;
        }
    }
}