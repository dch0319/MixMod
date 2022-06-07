using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Hearthstone.InGameMessage;

namespace FuckRestriction
{
    public class Patch
    {
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(SplashScreen), "GetRatingsScreenRegion")]
        public static IEnumerable<CodeInstruction> PatchRatingsScreenRegion(IEnumerable<CodeInstruction> instructions)
        {
            yield return new CodeInstruction(OpCodes.Ldc_I4_0);
            yield return new CodeInstruction(OpCodes.Ret);
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(GraphicsResolution), "IsAspectRatioWithinLimit")]
        public static IEnumerable<CodeInstruction> PatchRatioLimit(IEnumerable<CodeInstruction> instructions)
        {
            instructions.ToList()[1].opcode = OpCodes.Brtrue;
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(ViewCountController), "GetViewCount")]
        public static IEnumerable<CodeInstruction> PatchViewCount(IEnumerable<CodeInstruction> instructions)
        {
            instructions.ToList()[9].opcode = OpCodes.Ldc_I4_1;
            return instructions;
        }
    }
}