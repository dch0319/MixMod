using System.Reflection;
using Blizzard.GameService.SDK.Client.Integration;

namespace MixMod.Patches
{
	public static class SharedPlayerInfoPatch
	{
		private static FieldInfo m_gameAccountIdInfo = typeof(SharedPlayerInfo).GetField("m_gameAccountId", BindingFlags.Instance | BindingFlags.NonPublic);

		public static BnetGameAccountId GetGameAccountId(this SharedPlayerInfo __instance)
		{
			return m_gameAccountIdInfo?.GetValue(__instance) as BnetGameAccountId;
		}
	}
}
