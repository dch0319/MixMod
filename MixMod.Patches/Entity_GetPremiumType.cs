using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(Entity), "GetPremiumType")]
	public static class Entity_GetPremiumType
	{
		public static bool DoesDiamondModelExistOnCardDef(this Entity __instance)
		{
			using DefLoader.DisposableCardDef disposableCardDef = __instance.ShareDisposableCardDef();
			return disposableCardDef != null && (object)disposableCardDef.CardDef != null && !string.IsNullOrEmpty(disposableCardDef.CardDef.m_DiamondModel);
		}

		public static bool Prefix(Entity __instance, ref TAG_PREMIUM __result)
		{
			CardState dIAMOND = MixModConfig.Get().DIAMOND;
			CardState gOLDEN = MixModConfig.Get().GOLDEN;
			if (GameMgr.Get() != null && !GameMgr.Get().IsBattlegrounds() && GameState.Get() != null && GameState.Get().IsGameCreatedOrCreating())
			{
				if (__instance.DoesDiamondModelExistOnCardDef())
				{
					if (dIAMOND == CardState.All || (dIAMOND == CardState.OnlyMy && __instance.IsControlledByFriendlySidePlayer()))
					{
						__result = TAG_PREMIUM.DIAMOND;
						return false;
					}
					if (dIAMOND == CardState.Disabled)
					{
						__result = TAG_PREMIUM.NORMAL;
						return false;
					}
				}
				if (gOLDEN == CardState.All || (gOLDEN == CardState.OnlyMy && __instance.IsControlledByFriendlySidePlayer()))
				{
					__result = TAG_PREMIUM.GOLDEN;
					return false;
				}
				if (gOLDEN == CardState.Disabled)
				{
					__result = TAG_PREMIUM.NORMAL;
					return false;
				}
			}
			return true;
		}
	}
}
