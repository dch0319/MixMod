using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(EmoteHandler), "EmoteSpamBlocked")]
	public static class EmoteHandler_EmoteSpamBlocked
	{
		private static FieldInfo m_timeSinceLastEmoteInfo = typeof(EmoteHandler).GetField("m_timeSinceLastEmote", BindingFlags.Instance | BindingFlags.NonPublic);

		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = 2;
			list[num++].opcode = OpCodes.Brtrue_S;
			list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
			list.Insert(num++, new CodeInstruction(OpCodes.Ldfld, m_timeSinceLastEmoteInfo));
			list.Insert(num++, new CodeInstruction(OpCodes.Ldc_R4, 1.5f));
			Label label = generator.DefineLabel();
			list.Insert(num++, new CodeInstruction(OpCodes.Bge_Un_S, label));
			list[num].MoveLabelsFrom(list[num + 2]);
			num += 2;
			list.Insert(num, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
			list[num++].labels.Add(label);
			list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("ExtendedBM", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
			list.Insert(num, new CodeInstruction(OpCodes.Brtrue_S));
			list[num].operand = list[num + 3].operand;
			return list;
		}
	}
}
