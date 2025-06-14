using HarmonyLib;
using RimWorld;
using Verse;
using System.Collections.Generic;
using System;



namespace VanillaMemesExpanded
{
    public class ReflectionCache
    {
        public static readonly AccessTools.FieldRef<Pawn_GuestTracker, PrisonerInteractionModeDef> interactionMode =
           AccessTools.FieldRefAccess<Pawn_GuestTracker, PrisonerInteractionModeDef>(AccessTools.Field(typeof(Pawn_GuestTracker), "interactionMode"));

        public static readonly Func<ITab, Pawn> selPawn = (Func<ITab, Pawn>)Delegate.CreateDelegate(typeof(Func<ITab, Pawn>),
            AccessTools.PropertyGetter(typeof(ITab), "SelPawn"));
    }
}
