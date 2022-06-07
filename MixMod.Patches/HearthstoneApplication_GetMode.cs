using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Hearthstone;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(HearthstoneApplication), "GetMode")]
	public static class HearthstoneApplication_GetMode
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Brtrue);
			if (num > 0)
			{
				num++;
				Label label = generator.DefineLabel();
				list[num].labels.Add(label);
				if (list[num].opcode == OpCodes.Ldc_I4_2)
				{
					list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
					list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("DevEnabled", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
					list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
					list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
					list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("IsInternal", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
					Label label2 = generator.DefineLabel();
					list.Insert(num, new CodeInstruction(OpCodes.Brtrue_S, label2));
					num += 3;
					list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_1));
					list[num++].labels.Add(label2);
					list.Insert(num, new CodeInstruction(OpCodes.Ret));
				}
			}
			return list;
		}
	}
}
