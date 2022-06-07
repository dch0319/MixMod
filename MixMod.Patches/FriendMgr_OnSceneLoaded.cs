using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(FriendMgr), "OnSceneLoaded")]
	public static class FriendMgr_OnSceneLoaded
	{
		private static MethodInfo registerCreateGameListenerInfo = typeof(GameState).GetMethod("RegisterCreateGameListener", BindingFlags.Instance | BindingFlags.Public, null, new Type[2]
		{
			typeof(GameState.CreateGameCallback),
			typeof(object)
		}, null);

		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			int num = list.FindLastIndex((CodeInstruction x) => x.opcode == OpCodes.Ldftn && (x.operand as MethodInfo).Name == "OnGameOver");
			if (num > 0)
			{
				list.Insert(num++, new CodeInstruction(OpCodes.Ldftn, new Action<GameState.CreateGamePhase, object>(FriendMgrPatch.OnGameCreated).Method));
				list.Insert(num++, new CodeInstruction(OpCodes.Newobj, typeof(GameState.CreateGameCallback).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[2]
				{
					typeof(object),
					typeof(IntPtr)
				}, null)));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldnull));
				list.Insert(num++, new CodeInstruction(OpCodes.Callvirt, registerCreateGameListenerInfo));
				list.Insert(num++, new CodeInstruction(OpCodes.Pop));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldloc_0));
				list.Insert(num++, new CodeInstruction(OpCodes.Ldarg_0));
			}
			return list;
		}
	}
}
