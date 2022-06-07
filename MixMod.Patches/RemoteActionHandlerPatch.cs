using HarmonyLib;
using UnityEngine;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(RemoteActionHandler), "CanReceiveEnemyEmote")]
	public static class RemoteActionHandlerPatch
	{
		private static int m_lastPlayerId;

		private static float m_lastEnemyEmoteTime;

		private static int m_chainedEnemyEmotes;

		public static void Postfix(bool __result, EmoteType emoteType, int playerId)
		{
			int emotesBeforeBlock;
			if (!__result || !MixModConfig.Get().EmoteSpamBlocker || (emotesBeforeBlock = MixModConfig.Get().EmotesBeforeBlock) <= 0)
			{
				return;
			}
			float time = Time.time;
			if (m_lastPlayerId == playerId)
			{
				if (m_lastEnemyEmoteTime != 0f && time - m_lastEnemyEmoteTime < 9f)
				{
					m_chainedEnemyEmotes++;
					if (m_chainedEnemyEmotes > emotesBeforeBlock)
					{
						EnemyEmoteHandler.Get().SquelchPlayer(playerId);
					}
				}
				else
				{
					m_chainedEnemyEmotes = 1;
				}
			}
			else
			{
				m_chainedEnemyEmotes = 1;
				m_lastPlayerId = playerId;
			}
			m_lastEnemyEmoteTime = time;
		}
	}
}
