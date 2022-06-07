using System.Reflection;

namespace MixMod.Patches
{
	public static class EnemyEmoteHandlerPatch
	{
		private static MethodInfo doSquelchClickInfo = typeof(EnemyEmoteHandler).GetMethod("DoSquelchClick", BindingFlags.Instance | BindingFlags.NonPublic);

		private static FieldInfo m_squelchedInfo = typeof(EnemyEmoteHandler).GetField("m_squelched", BindingFlags.Instance | BindingFlags.NonPublic);

		public static void DoSquelchClick(this EnemyEmoteHandler __instance)
		{
			doSquelchClickInfo?.Invoke(__instance, null);
		}

		public static void SquelchPlayer(this EnemyEmoteHandler __instance, int playerId)
		{
			Map<int, bool> map = m_squelchedInfo.GetValue(__instance) as Map<int, bool>;
			if (map != null)
			{
				map[playerId] = true;
			}
		}
	}
}
