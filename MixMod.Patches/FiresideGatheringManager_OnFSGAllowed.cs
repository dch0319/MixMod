using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(FiresideGatheringManager), "OnFSGAllowed")]
	public static class FiresideGatheringManager_OnFSGAllowed
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Callvirt && (x.operand as MethodInfo).Name == "get_GPSOrWifiServicesAvailable");
			if (num > 0)
			{
				num++;
				object operand = list[num].operand;
				list.Insert(num++, new CodeInstruction(OpCodes.Brtrue_S, operand));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("FiresideGathering", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
			}
			return list;
		}
	}
}
