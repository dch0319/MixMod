using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MixMod.Patches
{
	public static class EmoteHandlerPatch
	{
		private static FieldInfo m_totalEmotesInfo = typeof(EmoteHandler).GetField("m_totalEmotes", BindingFlags.Instance | BindingFlags.NonPublic);

		private static FieldInfo m_availableEmotesInfo = typeof(EmoteHandler).GetField("m_availableEmotes", BindingFlags.Instance | BindingFlags.NonPublic);

		private static List<EmoteOption> m_FoundedEmotes;

		public static void HandleKeyboardInput(this EmoteHandler __instance, int EmoteIndex, bool useExtended = false)
		{
			if (EmoteHandler.Get().EmoteSpamBlocked())
			{
				return;
			}
			List<EmoteOption> list = m_availableEmotesInfo.GetValue(__instance) as List<EmoteOption>;
			if (useExtended)
			{
				if (!MixModConfig.Get().UseExtendedEmotes || EmoteIndex + 1 > m_FoundedEmotes.Count)
				{
					return;
				}
				m_totalEmotesInfo.SetValue(__instance, (int)m_totalEmotesInfo.GetValue(__instance) + 1);
				if (MixModConfig.Get().DisableRandomForEmotes || !GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
				{
					m_FoundedEmotes[EmoteIndex].DoClick();
					return;
				}
				List<EmoteOption> list2 = new List<EmoteOption>();
				foreach (EmoteOption item in list.Concat(__instance.m_HiddenEmotes))
				{
					if (item.CanPlayerUseEmoteType(GameState.Get().GetFriendlySidePlayer()))
					{
						list2.Add(item);
					}
				}
				foreach (EmoteOption foundedEmote in m_FoundedEmotes)
				{
					if (foundedEmote != null)
					{
						list2.Add(foundedEmote);
					}
				}
				if (list2.Count > 0)
				{
					list2[Random.Range(0, list2.Count)].DoClick();
				}
			}
			else
			{
				if (EmoteIndex + 1 > list.Count)
				{
					return;
				}
				m_totalEmotesInfo.SetValue(__instance, (int)m_totalEmotesInfo.GetValue(__instance) + 1);
				if (MixModConfig.Get().DisableRandomForEmotes || !GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
				{
					list[EmoteIndex].DoClick();
					return;
				}
				List<EmoteOption> list3 = new List<EmoteOption>();
				foreach (EmoteOption item2 in list.Concat(__instance.m_HiddenEmotes))
				{
					if (item2.CanPlayerUseEmoteType(GameState.Get().GetFriendlySidePlayer()))
					{
						list3.Add(item2);
					}
				}
				if (list3.Count > 0)
				{
					list3[Random.Range(0, list3.Count)].DoClick();
				}
			}
		}

		public static void DetermineFoundedEmotes(this EmoteHandler __instance)
		{
			if (m_FoundedEmotes != null && m_FoundedEmotes.Count != 0)
			{
				return;
			}
			m_FoundedEmotes = new List<EmoteOption>(11);
			EmoteOption emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_NEW_YEAR);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.HAPPY_NEW_YEAR,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_NEW_YEAR_LUNAR);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.HAPPY_NEW_YEAR_LUNAR,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_HOLIDAYS);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.HAPPY_HOLIDAYS,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_HALLOWEEN);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.HAPPY_HALLOWEEN,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.HAPPY_NOBLEGARDEN);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.HAPPY_NOBLEGARDEN,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_GREETINGS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_ONE);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_ONE,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_TWO);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_TWO,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_THREE);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_THREE,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_ONE);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_ONE,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_TWO);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_TWO,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			emoteOption = __instance.m_EmoteOverrides.FirstOrDefault((EmoteOption x) => x.m_EmoteType == EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_THREE);
			if (emoteOption == null)
			{
				emoteOption = new EmoteOption
				{
					m_EmoteType = EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_THREE,
					m_StringTag = "GAMEPLAY_EMOTE_LABEL_FIREWORKS"
				};
			}
			m_FoundedEmotes.Add(emoteOption);
			foreach (EmoteOption foundedEmote in m_FoundedEmotes)
			{
				foundedEmote.UpdateEmoteType();
			}
		}
	}
}
