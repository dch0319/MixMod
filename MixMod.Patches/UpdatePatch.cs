using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MixMod.Patches
{
	[HarmonyPatch]
	public static class UpdatePatch
	{
		private static IEnumerable<MethodBase> TargetMethods()
		{
			yield return typeof(MatchingQueueTab).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
			yield return typeof(ThinkEmoteManager).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			MethodInfo deltaTimeInfo = typeof(Time).GetProperty("deltaTime", BindingFlags.Static | BindingFlags.Public).GetGetMethod();
			MethodInfo getMethod = typeof(Time).GetProperty("unscaledDeltaTime", BindingFlags.Static | BindingFlags.Public).GetGetMethod();
			int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Call && x.operand as MethodInfo == deltaTimeInfo);
			if (num > 0)
			{
				list[num].operand = getMethod;
			}
			return list;
		}
	}
}
