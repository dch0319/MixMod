using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(PlayerLeaderboardManager), "SetCurrentOpponent")]
	public static class PlayerLeaderboardManager_SetCurrentOpponent
	{
		public static void Postfix(int opponentPlayerId)
		{
			if (opponentPlayerId != -1)
			{
				PlayerLeaderboardManagerPatch.UpdateCurrentOpponent(opponentPlayerId);
			}
		}
	}
}
