using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(Actor), "GetPremium")]
	public static class Actor_GetPremium
	{
		public static bool Prefix(Actor __instance, Entity ___m_entity, ref TAG_PREMIUM __result)
		{
			CardState dIAMOND = MixModConfig.Get().DIAMOND;
			CardState gOLDEN = MixModConfig.Get().GOLDEN;
			if (GameMgr.Get() != null && !GameMgr.Get().IsBattlegrounds() && GameState.Get() != null && GameState.Get().IsGameCreatedOrCreating())
			{
				if (__instance.DoesDiamondModelExistOnCardDef())
				{
					if (dIAMOND == CardState.All || (dIAMOND == CardState.OnlyMy && ___m_entity.IsControlledByFriendlySidePlayer()))
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
				if (gOLDEN == CardState.All || (gOLDEN == CardState.OnlyMy && ___m_entity.IsControlledByFriendlySidePlayer()))
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
