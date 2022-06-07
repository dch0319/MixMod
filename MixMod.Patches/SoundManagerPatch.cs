using System.Reflection;

namespace MixMod.Patches
{
	public static class SoundManagerPatch
	{
		public static bool MuteKeyPressed = false;

		private static MethodInfo updateAppMuteInfo = typeof(SoundManager).GetMethod("UpdateAppMute", BindingFlags.Instance | BindingFlags.NonPublic);

		public static void OnMuteKeyPressed()
		{
			MuteKeyPressed = !MuteKeyPressed;
			SoundManager soundManager = SoundManager.Get();
			if (soundManager != null)
			{
				updateAppMuteInfo?.Invoke(soundManager, null);
			}
		}
	}
}
