using HarmonyLib;

namespace MixMod.Patches
{
	[HarmonyPatch(typeof(FiresideGatheringManager), "RequestNearbyFSGs")]
	public static class FiresideGatheringManager_RequestNearbyFSGs
	{
		public static bool Prefix(ref bool ___m_isRequestNearbyFSGsPending)
		{
			if (MixModConfig.Get().FiresideGathering)
			{
				___m_isRequestNearbyFSGsPending = true;
				Network.Get().RequestNearbyFSGs(MixModConfig.Get().Latitude, MixModConfig.Get().Longitude, MixModConfig.Get().GpsAccuracy, null);
				return false;
			}
			return true;
		}
	}
}
