using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(ZoneHand), "GetCardRotation", new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public static class ZoneHand_GetCardRotation
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "IsRevealed");
			if (num > 0)
			{
				num++;
				object operand = list[num++].operand;
				Label label = generator.DefineLabel();
				list[num].labels.Add(label);
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("MoveEnemyCards", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_0));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_1));
				list.Insert(num++, new CodeInstruction(OpCodes.Conv_R4));
				list.Insert(num++, new CodeInstruction(OpCodes.Mul));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_1));
				list.Insert(num++, new CodeInstruction(OpCodes.Add));
				list.Insert(num++, new CodeInstruction(OpCodes.Stloc_3));
				list.Insert(num, new CodeInstruction(OpCodes.Br_S, operand));
			}
			return list;
		}
	}
}
