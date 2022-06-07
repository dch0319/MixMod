using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(CoinManager), "InitCoinDataWhenReady", MethodType.Enumerator)]
	public static class CoinManager_InitCoinDataWhenReady
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Stfld);
			LocalBuilder operand = generator.DeclareLocal(typeof(TAG_PREMIUM));
			if (num > 0)
			{
				Label label = generator.DefineLabel();
				Label label2 = generator.DefineLabel();
				Label label3 = generator.DefineLabel();
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("DevEnabled", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("GoldenCoin", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_1));
				list.Insert(num++, new CodeInstruction(OpCodes.Beq_S, label2));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("GoldenCoin", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_2));
				list.Insert(num++, new CodeInstruction(OpCodes.Beq_S, label2));
				list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_0));
				list[num++].labels.Add(label);
				list.Insert(num++, new CodeInstruction(OpCodes.Br_S, label3));
				list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_1));
				list[num++].labels.Add(label2);
				list.Insert(num, new CodeInstruction(OpCodes.Stloc_S, operand));
				list[num].labels.Add(label3);
			}
			num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Newobj && (x.operand as ConstructorInfo).Name == "CollectibleCard");
			if (num > 0)
			{
				num--;
				list[num].opcode = OpCodes.Ldloc_S;
				list[num].operand = operand;
			}
			return list;
		}
	}
}
