using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(PackOpening), "AutomaticallyOpenPack")]
	public static class PackOpening_AutomaticallyOpenPack
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Ret);
			if (num > 0)
			{
				Label label = generator.DefineLabel();
				list[num].labels.Add(label);
				list.Insert(num++, new CodeInstruction(OpCodes.Call, new Func<MixModConfig>(MixModConfig.Get).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, typeof(MixModConfig).GetProperty("QuickPackOpeningEnabled", BindingFlags.Instance | BindingFlags.Public).GetGetMethod()));
				list.Insert(num++, new CodeInstruction(OpCodes.Brfalse_S, label));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldfld, typeof(PackOpening).GetField("m_director", BindingFlags.Instance | BindingFlags.NonPublic)));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, new Func<PackOpeningDirector, IEnumerator>(PackOpeningDirectorPatch.ForceRevealAllCards).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Call, typeof(MonoBehaviour).GetMethod("StartCoroutine", BindingFlags.Instance | BindingFlags.Public, null, new Type[1]
				{
					typeof(IEnumerator)
				}, null)));
				list.Insert(num, new CodeInstruction(OpCodes.Pop));
			}
			return list;
		}
	}
}
