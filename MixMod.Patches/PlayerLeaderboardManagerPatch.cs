using System.Reflection;
using Blizzard.GameService.SDK.Client.Integration;

namespace MixMod.Patches
{
	public static class PlayerLeaderboardManagerPatch
	{
		private static FieldInfo m_currentlyMousedOverTileInfo = typeof(PlayerLeaderboardManager).GetField("m_currentlyMousedOverTile", BindingFlags.Instance | BindingFlags.NonPublic);

		private static BnetPlayer m_currentOpponent;

		public static void UpdateCurrentOpponent(int opponentPlayerId)
		{
			if (GameState.Get() == null || !GameState.Get().GetPlayerInfoMap().ContainsKey(opponentPlayerId))
			{
				m_currentOpponent = null;
				return;
			}
			BnetGameAccountId gameAccountId = GameState.Get().GetPlayerInfoMap()[opponentPlayerId].GetGameAccountId();
			if (gameAccountId == null)
			{
				m_currentOpponent = null;
			}
			else
			{
				m_currentOpponent = BnetPresenceMgr.Get().GetPlayer(gameAccountId);
			}
		}

		public static BnetPlayer GetCurrentOpponent(this PlayerLeaderboardManager __instance)
		{
			return m_currentOpponent;
		}

		public static BnetPlayer GetSelectedOpponent(this PlayerLeaderboardManager __instance)
		{
			PlayerLeaderboardCard playerLeaderboardCard = m_currentlyMousedOverTileInfo?.GetValue(__instance) as PlayerLeaderboardCard;
			if ((object)playerLeaderboardCard == null || GameState.Get() == null)
			{
				return null;
			}
			int tag = playerLeaderboardCard.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
			if (!GameState.Get().GetPlayerInfoMap().ContainsKey(tag))
			{
				return null;
			}
			BnetGameAccountId gameAccountId = GameState.Get().GetPlayerInfoMap()[tag].GetGameAccountId();
			if (gameAccountId == null)
			{
				return null;
			}
			return BnetPresenceMgr.Get().GetPlayer(gameAccountId);
		}
	}
}
