using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(PackOpeningDirector), "Awake")]
	public static class PackOpeningDirector_Awake
	{
		public static void Postfix()
		{
			PackOpeningDirectorPatch.m_WaitingForCards = true;
		}
	}
}
