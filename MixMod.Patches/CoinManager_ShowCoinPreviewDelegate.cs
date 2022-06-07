using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace MixMod.Patches
{
	[HarmonyPatch]
	public static class CoinManager_ShowCoinPreviewDelegate
	{
		public static MethodBase TargetMethod()
		{
			MethodDefinition methodDefinition = typeof(CoinManager).GetMethod("ShowCoinPreview", BindingFlags.Instance | BindingFlags.Public).ToDefinition().Body.Instructions.Last((Instruction x) => x.OpCode == Mono.Cecil.Cil.OpCodes.Ldftn).Operand as MethodDefinition;
			return typeof(CoinManager).Module.ResolveMethod(methodDefinition.MetadataToken.ToInt32());
		}

		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == System.Reflection.Emit.OpCodes.Ldc_I4_0);
			if (num > 0)
			{
				LocalBuilder operand = generator.DeclareLocal(typeof(TAG_PREMIUM));
				list[num].opcode = System.Reflection.Emit.OpCodes.Ldloc_S;
				list[num].operand = operand;
				num = 0;
				Label label = generator.DefineLabel();
				Label label2 = generator.DefineLabel();
				Label label3 = generator.DefineLabel();
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Callvirt, typeof(MixModConfig).GetProperty("DevEnabled", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Brfalse_S, label));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Callvirt, typeof(MixModConfig).GetProperty("GoldenCoin", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Ldc_I4_1));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Beq_S, label2));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Callvirt, typeof(MixModConfig).GetProperty("GoldenCoin", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Ldc_I4_2));
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Beq_S, label2));
				list.Insert(num, new CodeInstruction(System.Reflection.Emit.OpCodes.Ldc_I4_0));
				list[num++].labels.Add(label);
				list.Insert(num++, new CodeInstruction(System.Reflection.Emit.OpCodes.Br_S, label3));
				list.Insert(num, new CodeInstruction(System.Reflection.Emit.OpCodes.Ldc_I4_1));
				list[num++].labels.Add(label2);
				list.Insert(num, new CodeInstruction(System.Reflection.Emit.OpCodes.Stloc_S, operand));
				list[num].labels.Add(label3);
			}
			return list;
		}
	}
}
