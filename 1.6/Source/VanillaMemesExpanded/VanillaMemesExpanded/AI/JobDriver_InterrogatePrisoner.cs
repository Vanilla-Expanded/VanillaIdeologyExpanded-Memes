using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace VanillaMemesExpanded
{
    public class JobDriver_InterrogatePrisoner : JobDriver
    {
        public static IntRange CountRange = new(2, 5);
        protected Pawn Interrogatee => job.targetA.Pawn;

        public override bool TryMakePreToilReservations(bool errorOnFailed) => pawn.Reserve(job.targetA, job, 1, -1, null, errorOnFailed);

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedOrNull(TargetIndex.A);
            this.FailOnMentalState(TargetIndex.A);
            this.FailOnNotAwake(TargetIndex.A);
            this.FailOn(() => !Interrogatee.IsPrisonerOfColony || !Interrogatee.guest.PrisonerIsSecure);
            yield return Toils_General.Do(() => job.count = CountRange.RandomInRange);
            var gotoPrisoner = Toils_Interpersonal.GotoPrisoner(pawn, Interrogatee, Interrogatee.guest.ExclusiveInteractionMode);
            yield return gotoPrisoner;
            yield return Toils_Interpersonal.WaitToBeAbleToInteract(pawn);
            yield return Toils_Interpersonal.GotoInteractablePosition(TargetIndex.A);
            yield return Toils_Interpersonal.ConvinceRecruitee(pawn, Interrogatee, InternalDefOf.VFEA_Intimidate);
            yield return Toils_Misc.ThrowColonistAttackingMote(TargetIndex.A);
            yield return Toils_Combat.FollowAndMeleeAttack(TargetIndex.A, TargetIndex.None, delegate
            {
                if (pawn.meleeVerbs.TryMeleeAttack(Interrogatee,
                    pawn.meleeVerbs.GetUpdatedAvailableVerbsList(false).Where(ve => ve.verb.GetDamageDef() == DamageDefOf.Blunt).RandomElement().verb)) ReadyForNextToil();
            });
            yield return Toils_General.Do(() => job.count--);
            yield return Toils_Jump.JumpIf(gotoPrisoner, () => job.count > 0);
            yield return Toils_Interpersonal.GotoPrisoner(pawn, Interrogatee, Interrogatee.guest.ExclusiveInteractionMode);
            yield return Toils_Interpersonal.GotoInteractablePosition(TargetIndex.A);
            yield return Toils_Interpersonal.SetLastInteractTime(TargetIndex.A);
            yield return new Toil
            {
                initAction = delegate
                {
                    if (!pawn.Spawned || !pawn.Awake()) return;

                    pawn.interactions.TryInteractWith(Interrogatee, InternalDefOf.VFEA_InterrogatePrisoner);
                },
                socialMode = RandomSocialMode.Off,
                defaultCompleteMode = ToilCompleteMode.Delay,
                defaultDuration = 350,
                activeSkill = () => SkillDefOf.Social
            };
        }
    }
}