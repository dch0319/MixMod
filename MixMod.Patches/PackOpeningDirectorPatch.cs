using System.Collections;
using System.Reflection;
using Game.PackOpening;
using UnityEngine;

namespace MixMod.Patches
{
	public static class PackOpeningDirectorPatch
	{
		public static bool m_WaitingForCards;

		public static bool m_WaitingForAllCardsRevealed;

		private static FieldInfo m_hiddenCardsInfo = typeof(PackOpeningDirector).GetField("m_hiddenCards", BindingFlags.Instance | BindingFlags.NonPublic);

		public static IEnumerator ForceRevealAllCards(this PackOpeningDirector __instance)
		{
			object value = m_hiddenCardsInfo.GetValue(__instance);
			HiddenCards m_hiddenCards = value as HiddenCards;
			if (m_hiddenCards != null)
			{
				m_WaitingForAllCardsRevealed = true;
				while (m_WaitingForCards)
				{
					yield return new WaitForSeconds(0.05f);
				}
				m_hiddenCards.ForceRevealAllCards();
				m_WaitingForAllCardsRevealed = false;
			}
		}
	}
}
