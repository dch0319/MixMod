using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Hearthstone;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(HearthstoneApplication), "Job_InitializeMode", MethodType.Enumerator)]
	public static class HearthstoneApplication_Job_InitializeMode
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Brfalse);
			if (num > 0)
			{
				object operand = list[num].operand;
				if (operand is Label)
				{
					Label label = (Label)operand;
					Label label2 = generator.DefineLabel();
					list[num].operand = label2;
					num += 3;
					if (list[num].opcode == OpCodes.Ldc_I4_2)
					{
						list.Insert(num, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
						list[num++].labels.Add(label2);
						list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("DevEnabled", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
						list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
						list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
						list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("IsInternal", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
						Label label3 = generator.DefineLabel();
						list.Insert(num, new CodeInstruction(OpCodes.Brtrue_S, label3));
						num += 2;
						Label label4 = generator.DefineLabel();
						list.Insert(num++, new CodeInstruction(OpCodes.Br_S, label4));
						list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_1));
						list[num++].labels.Add(label3);
						list[num].labels.Add(label4);
					}
				}
			}
			return list;
		}
	}
}
