using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(NameBanner), "UpdateMedalWhenReady", MethodType.Enumerator)]
	public static class NameBanner_UpdateMedalWhenReady
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "get_ShowOpponentRankInGame");
			if (num > 0)
			{
				num++;
				object operand = list[num].operand;
				list.Insert(num++, new CodeInstruction(OpCodes.Brtrue_S, operand));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("ShowOpponentRankInGame", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
			}
			return list;
		}
	}
}
