using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(ZoneHand), "GetCardScale")]
	public static class ZoneHand_GetCardScale
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Ldfld && (x.operand as FieldInfo).Name == "enemyHand");
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
				list.Insert(num++, new CodeInstruction(OpCodes.Ldfld, typeof(ZoneHand).GetField("m_controller", BindingFlags.Instance | BindingFlags.NonPublic)));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(Player).GetMethod("IsRevealed", BindingFlags.Instance | BindingFlags.Public)));
				list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 0.41f));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 0.085f));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 0.41f));
				list.Insert(num++, new CodeInstruction(OpCodes.Newobj, typeof(Vector3).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[3]
				{
					typeof(float),
					typeof(float),
					typeof(float)
				}, null)));
				list.Insert(num++, new CodeInstruction(OpCodes.Ret));
				list.Insert(num, new CodeInstruction(OpCodes.Br_S, operand));
			}
			return list;
		}
	}
}
