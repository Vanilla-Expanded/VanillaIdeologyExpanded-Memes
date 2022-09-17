using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;



namespace VanillaMemesExpanded
{


    [HarmonyPatch(typeof(Precept_Role))]
    [HarmonyPatch("DisabledWorkTypes", MethodType.Getter)]
    public static class VanillaMemesExpanded_Precept_Role_DisabledWorkTypes_Patch
    {
        [HarmonyPostfix]
        public static IEnumerable<WorkTypeDef> DisableDumbLabor(IEnumerable<WorkTypeDef> values, Precept_Role __instance)
        {
         
            if (__instance.ideo.HasPrecept(InternalDefOf.VME_DumbLabor_Horrible)){
               
                if(__instance.def.defName== "IdeoRole_Moralist"|| __instance.def.defName == "IdeoRole_Leader" || __instance.def.defName == "VME_IdeoRole_LeaderThird"
                    || __instance.def.defName == "VME_IdeoRole_LeaderSecond")
                {
                   
                    List<WorkTypeDef> resultingList = values.ToList();

                    foreach (WorkTypeDef worktype in DefDatabase<WorkTypeDef>.AllDefsListForReading) { 
                        if (worktype.workTags.HasFlag(WorkTags.ManualDumb))
                        {
                            if (!resultingList.Contains(worktype)) {
                                resultingList.Add(worktype);
                            }
                        }
                    
                    
                    }
                    //Log.Message(resultingList.ToStringSafeEnumerable());
                    return resultingList;

                }
                else return values;


            } else return values;





        }
    }








}
