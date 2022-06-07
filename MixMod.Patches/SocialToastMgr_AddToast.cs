using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(SocialToastMgr), "AddToast", new Type[]
	{
		typeof(UserAttentionBlocker),
		typeof(string),
		typeof(SocialToastMgr.TOAST_TYPE),
		typeof(float),
		typeof(bool)
	})]
	public static class SocialToastMgr_AddToast
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindIndex((CodeInstruction x) => x.opcode == OpCodes.Ret);
			if (num > 0)
			{
				num++;
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_3));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, typeof(Time).GetProperty("timeScale", BindingFlags.Static | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(OpCodes.Mul));
				list.Insert(num++, new CodeInstruction(OpCodes.Starg_S, (byte)3));
			}
			return list;
		}
	}
}
