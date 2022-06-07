using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch]
	public static class GetPremiumPatch
	{
		private static IEnumerable<MethodBase> TargetMethods()
		{
			yield return typeof(Card).GetMethod("GetPremium", BindingFlags.Instance | BindingFlags.Public);
			yield return typeof(CollectionDraggableCardVisual).GetMethod("GetPremium", BindingFlags.Instance | BindingFlags.Public);
			yield return typeof(HeroPickerButton).GetMethod("GetPremium", BindingFlags.Instance | BindingFlags.Public);
			yield return typeof(RewardExtensions).GetMethod("GetPremium", BindingFlags.Static | BindingFlags.Public);
			yield return typeof(SpawnToDeckSpell).GetMethod("GetPremium", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		public static bool Prefix(ref TAG_PREMIUM __result)
		{
			if (MixModConfig.Get().DevEnabled)
			{
				CardState goldenCoin = MixModConfig.Get().GoldenCoin;
				if (GameMgr.Get() != null && !GameMgr.Get().IsBattlegrounds() && GameState.Get() != null && GameState.Get().IsGameCreatedOrCreating())
				{
					switch (goldenCoin)
					{
					case CardState.OnlyMy:
					case CardState.All:
						__result = TAG_PREMIUM.GOLDEN;
						return false;
					case CardState.Disabled:
						__result = TAG_PREMIUM.NORMAL;
						return false;
					}
				}
			}
			return true;
		}
	}
}
