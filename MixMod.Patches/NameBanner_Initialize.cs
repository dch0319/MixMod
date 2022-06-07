using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(NameBanner), "Initialize")]
	public static class NameBanner_Initialize
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Call && (x.operand as MethodInfo).Name == "IsGameTypeRanked");
			if (num > 0)
			{
				num++;
				Label label = generator.DefineLabel();
				list[num].labels.Add(label);
				Label label2 = generator.DefineLabel();
				list.Insert(num++, new CodeInstruction(OpCodes.Brtrue_S, label2));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("ShowOpponentRankInGame", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(OpCodes.Br_S, label));
				list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_1));
				list[num].labels.Add(label2);
			}
			return list;
		}
	}
}
