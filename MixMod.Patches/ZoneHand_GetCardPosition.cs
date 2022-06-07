using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(ZoneHand), "GetCardPosition", new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public static class ZoneHand_GetCardPosition
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
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldflda, typeof(ZoneHand).GetField("centerOfHand", BindingFlags.Instance | BindingFlags.NonPublic)));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldfld, typeof(Vector3).GetField("z", BindingFlags.Instance | BindingFlags.Public)));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_1));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_3));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_2));
				list.Insert(num++, new CodeInstruction(OpCodes.Div));
				list.Insert(num++, new CodeInstruction(OpCodes.Sub));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<int, int>(Mathf.Abs).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Conv_R4));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 2f));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<float, float, float>(Mathf.Pow).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_4));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_3));
				list.Insert(num++, new CodeInstruction(OpCodes.Mul));
				list.Insert(num++, new CodeInstruction(OpCodes.Conv_R4));
				list.Insert(num++, new CodeInstruction(OpCodes.Div));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_2));
				list.Insert(num++, new CodeInstruction(OpCodes.Mul));
				list.Insert(num++, new CodeInstruction(OpCodes.Sub));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_S, (byte)6));
				list.Insert(num++, new CodeInstruction(OpCodes.Sub));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 0.6f));
				list.Insert(num++, new CodeInstruction(OpCodes.Sub));
				list.Insert(num++, new CodeInstruction(OpCodes.Stloc_S, (byte)9));
				list.Insert(num, new CodeInstruction(OpCodes.Br_S, operand));
			}
			return list;
		}
	}
}
