using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Blizzard.T5.Configuration;
using HarmonyLib;
using Hearthstone.Login;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(DesktopLoginTokenFetcher), "GetTokenFromTokenFetcher")]
	public static class DesktopLoginTokenFetcher_GetTokenFromTokenFetcher
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			list.RemoveAt(0);
			list.InsertRange(0, new CodeInstruction[4]
			{
				new CodeInstruction(OpCodes.Ldstr, "Aurora.VerifyWebCredentials"),
				new CodeInstruction(OpCodes.Call, new Func<string, VarKey>(Vars.Key).Method),
				new CodeInstruction(OpCodes.Ldnull),
				new CodeInstruction(OpCodes.Callvirt, typeof(VarKey).GetMethod("GetStr", BindingFlags.Instance | BindingFlags.Public))
			});
			return list;
		}
	}
}
