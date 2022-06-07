using HarmonyLib;
using UnityEngine;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(TimeScaleMgr), "Update")]
	public static class TimeScaleMgr_Update
	{
		public static bool Prefix(float ___m_timeScaleMultiplier, float ___m_gameTimeScale)
		{
			if (!MixModConfig.Get().TimeScaleEnabled)
			{
				return true;
			}
			float timeScale = MixModConfig.Get().TimeScale;
			Time.timeScale = ((timeScale > ___m_timeScaleMultiplier) ? ((timeScale + (___m_timeScaleMultiplier - 1f) * 0.5f) * ___m_gameTimeScale) : ((___m_timeScaleMultiplier + (timeScale - 1f) * 0.5f) * ___m_gameTimeScale));
			return false;
		}
	}
}
