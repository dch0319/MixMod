using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(EnemyEmoteHandler), "Awake")]
	public static class EnemyEmoteHandler_Awake
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			generator.DeclareLocal(typeof(bool));
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Stfld && (x.operand as FieldInfo).Name == "m_squelched");
			if (num > 0)
			{
				num++;
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("EmoteSpamBlocker", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				Label label = generator.DefineLabel();
				list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("EmotesBeforeBlock", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_0));
				list.Insert(num++, new CodeInstruction(OpCodes.Ceq));
				Label label2 = generator.DefineLabel();
				list.Insert(num++, new CodeInstruction(OpCodes.Br_S, label2));
				list.Insert(num, new CodeInstruction(OpCodes.Ldc_I4_0));
				list[num++].labels.Add(label);
				list.Insert(num, new CodeInstruction(OpCodes.Stloc_1));
				list[num].labels.Add(label2);
				num += 9;
				list[num].opcode = OpCodes.Ldloc_1;
			}
			return list;
		}
	}
}
