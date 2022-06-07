using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(FiresideGatheringLocationHelperDialog), "Start")]
	public static class FiresideGatheringLocationHelperDialog_Start
	{
		private static MethodInfo ChangeStateInfo = typeof(FiresideGatheringLocationHelperDialog).GetMethod("ChangeState", BindingFlags.Instance | BindingFlags.NonPublic);

		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Ldfld && (x.operand as FieldInfo).Name == "m_isCheckInFailure");
			if (num > 0)
			{
				num--;
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("FiresideGathering", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				Label label = generator.DefineLabel();
				list[num].labels.Add(label);
				list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldc_I4_3));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, ChangeStateInfo));
				list.Insert(num++, new CodeInstruction(OpCodes.Ret));
			}
			return list;
		}
	}
}
