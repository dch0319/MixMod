namespace MixMod.Patches
{
	public static class FriendMgrPatch
	{
		private static BnetPlayer m_currentOpponent;

		public static BnetPlayer GetCurrentOpponent(this FriendMgr __instance)
		{
			return m_currentOpponent;
		}

		private static void UpdateCurrentOpponent()
		{
			if (GameState.Get() == null)
			{
				m_currentOpponent = null;
				return;
			}
			Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
			if (opposingSidePlayer == null)
			{
				m_currentOpponent = null;
			}
			else
			{
				m_currentOpponent = BnetPresenceMgr.Get().GetPlayer(opposingSidePlayer.GetGameAccountId());
			}
		}

		public static void OnGameCreated(GameState.CreateGamePhase phase, object userData)
		{
			GameState.Get().UnregisterCreateGameListener(OnGameCreated, null);
			UpdateCurrentOpponent();
		}
	}
}
