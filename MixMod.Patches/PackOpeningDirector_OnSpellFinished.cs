using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(PackOpeningDirector), "OnSpellFinished")]
	public static class PackOpeningDirector_OnSpellFinished
	{
		public static void Postfix()
		{
			PackOpeningDirectorPatch.m_WaitingForCards = false;
		}
	}
}
