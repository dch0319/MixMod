using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(PlayerLeaderboardManager), "SetNextOpponent")]
	public static class PlayerLeaderboardManager_SetNextOpponent
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Ret);
			if (num > 0)
			{
				num += 2;
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_1));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Action<int>(PlayerLeaderboardManagerPatch.UpdateCurrentOpponent).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
			}
			return list;
		}
	}
}
