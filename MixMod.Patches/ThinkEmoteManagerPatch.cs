using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(ThinkEmoteManager), "Update")]
	public static class ThinkEmoteManagerPatch
	{
		public static bool Prefix()
		{
			return !MixModConfig.Get().DisableThinkEmotes;
		}
	}
}
