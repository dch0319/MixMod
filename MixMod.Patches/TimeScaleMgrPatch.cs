using System.Reflection;

namespace MixMod.Patches
{
	public static class TimeScaleMgrPatch
	{
		private static MethodInfo updateInfo = typeof(TimeScaleMgr).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);

		public static void Update(this TimeScaleMgr __instance)
		{
			updateInfo?.Invoke(__instance, null);
		}
	}
}
