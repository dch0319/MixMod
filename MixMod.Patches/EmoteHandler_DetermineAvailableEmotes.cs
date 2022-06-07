using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(EmoteHandler), "DetermineAvailableEmotes")]
	public static class EmoteHandler_DetermineAvailableEmotes
	{
		public static void Postfix(EmoteHandler __instance)
		{
			__instance.DetermineFoundedEmotes();
		}
	}
}
