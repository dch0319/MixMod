using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(SoundManager), "UpdateAllMutes")]
	public static class SoundManager_UpdateAllMutes
	{
		public static bool Prefix(ref bool ___m_mute)
		{
			___m_mute = SoundManagerPatch.MuteKeyPressed | ___m_mute;
			return true;
		}
	}
}
