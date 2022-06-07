using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(PackOpening), "HandleKeyboardInput")]
	public static class PackOpening_HandleKeyboardInput
	{
		public static bool Prefix(ref bool __result)
		{
			if (PackOpeningDirectorPatch.m_WaitingForAllCardsRevealed)
			{
				__result = false;
				return false;
			}
			return true;
		}
	}
}
