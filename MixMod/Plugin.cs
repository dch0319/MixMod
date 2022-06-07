using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using MixMod.Patches;
using UnityEngine;

namespace MixMod
{
	[BepInPlugin("MixMod", "MixMod", "1.0.1")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			MixModConfig.Load(base.Config);
			MixModConfig.Get().timeScaleEnabledEntry.SettingChanged += delegate
			{
				TimeScaleMgr.Get().Update();
			};
			MixModConfig.Get().timeScaleEntry.SettingChanged += delegate
			{
				TimeScaleMgr.Get().Update();
			};
			Harmony harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
			TimeScaleMgr.Get().Update();
			base.Logger.LogInfo(string.Format("Plugin {0} is loaded! (Patched {1} methods)", "MixMod", harmony.GetPatchedMethods().Count()));
		}

		private void Update()
		{
			if (!MixModConfig.Get().EnableShortcuts || !Input.anyKey)
			{
				return;
			}
			if (SoundManager.Get() != null && MixModConfig.Get().SoundMuteShortcut.IsDown())
			{
				SoundManagerPatch.OnMuteKeyPressed();
			}
			else
			{
				if (!MixModConfig.Get().TimeScaleEnabled)
				{
					return;
				}
				if (MixModConfig.Get().ResetTimeScaleShortcut.IsDown())
				{
					MixModConfig.Get().TimeScale = 1f;
				}
				else if (MixModConfig.Get().MaxTimeScaleShortcut.IsDown())
				{
					float timeScale = 8f;
					MixModConfig.Get().TimeScale = timeScale;
				}
				else if (MixModConfig.Get().DoubleTimeScaleShortcut.IsDown())
				{
					float num = MixModConfig.Get().TimeScale + 1f;
					float num2 = 8f;
					if (num > num2)
					{
						num = num2;
					}
					MixModConfig.Get().TimeScale = num;
				}
				else if (MixModConfig.Get().DevideTimeScaleShortcut.IsDown())
				{
					float num3 = MixModConfig.Get().TimeScale - 1f;
					float num4 = 1f;
					if (num3 < num4)
					{
						num3 = num4;
					}
					MixModConfig.Get().TimeScale = num3;
				}
				else if (MixModConfig.Get().SimulateDisconnectShortcut.IsDown())
				{
					Network.Get().SimulateUncleanDisconnectFromGameServer();
				}
				else
				{
					if (GameState.Get() == null || GameMgr.Get() == null)
					{
						return;
					}
					if (GameMgr.Get().IsBattlegrounds() && MixModConfig.Get().ShutUpBobShortcut.IsDown())
					{
						MixModConfig.Get().ShutUpBob = !MixModConfig.Get().ShutUpBob;
					}
					else
					{
						if (!GameState.Get().IsGameCreated())
						{
							return;
						}
						if (!GameMgr.Get().IsSpectator())
						{
							if (MixModConfig.Get().ConcedeShortcut.IsDown())
							{
								GameState.Get().Concede();
								return;
							}
							if (GameState.Get().IsMulliganManagerActive() && MulliganManager.Get().GetMulliganButton() != null && MixModConfig.Get().ContinueMulliganShortcut.IsDown())
							{
								MulliganManager.Get().AutomaticContinueMulligan();
								return;
							}
						}
						if (GameMgr.Get().IsBattlegrounds() && MixModConfig.Get().CopySelectedBattleTagShortcut.IsDown() && PlayerLeaderboardManager.Get() != null && PlayerLeaderboardManager.Get().IsMousedOver())
						{
							BnetPlayer selectedOpponent = PlayerLeaderboardManager.Get().GetSelectedOpponent();
							if (selectedOpponent != null)
							{
								BnetBattleTag battleTag = selectedOpponent.GetBattleTag();
								if (battleTag != null)
								{
									string @string = battleTag.GetString();
									ClipboardUtils.CopyToClipboard(@string);
									UIStatus.Get().AddInfo(@string);
								}
							}
						}
						else if (MixModConfig.Get().CopyBattleTagShortcut.IsDown())
						{
							BnetPlayer bnetPlayer = null;
							if (GameMgr.Get().IsBattlegrounds())
							{
								if (PlayerLeaderboardManager.Get() != null)
								{
									bnetPlayer = PlayerLeaderboardManager.Get().GetCurrentOpponent();
								}
							}
							else if (FriendMgr.Get() != null)
							{
								bnetPlayer = FriendMgr.Get().GetCurrentOpponent();
							}
							if (bnetPlayer != null)
							{
								BnetBattleTag battleTag2 = bnetPlayer.GetBattleTag();
								if (battleTag2 != null)
								{
									string string2 = battleTag2.GetString();
									ClipboardUtils.CopyToClipboard(string2);
									UIStatus.Get().AddInfo(string2);
								}
							}
						}
						else
						{
							if (!GameState.Get().IsMainPhase())
							{
								return;
							}
							if (MixModConfig.Get().SquelchShortcut.IsDown())
							{
								EnemyEmoteHandler.Get().DoSquelchClick();
							}
							else
							{
								if (GameMgr.Get().IsSpectator())
								{
									return;
								}
								if (MixModConfig.Get().EndTurnShortcut.IsDown())
								{
									InputManager.Get().DoEndTurnButton();
								}
								else if (!(EmoteHandler.Get() == null))
								{
									if (MixModConfig.Get().GreetingsEmoteShortcut.IsDown())
									{
										EmoteHandler.Get().HandleKeyboardInput(0);
									}
									else if (MixModConfig.Get().WellPlayedEmoteShortcut.IsDown())
									{
										EmoteHandler.Get().HandleKeyboardInput(1);
									}
									else if (MixModConfig.Get().ThanksEmoteShortcut.IsDown())
									{
										EmoteHandler.Get().HandleKeyboardInput(2);
									}
									else if (MixModConfig.Get().WowEmoteShortcut.IsDown())
									{
										EmoteHandler.Get().HandleKeyboardInput(3);
									}
									else if (MixModConfig.Get().OopsEmoteShortcut.IsDown())
									{
										EmoteHandler.Get().HandleKeyboardInput(4);
									}
									else if (MixModConfig.Get().ThreatenEmoteShortcut.IsDown())
									{
										EmoteHandler.Get().HandleKeyboardInput(5);
									}
								}
							}
						}
					}
				}
			}
		}

        private static Harmony _patch;

        private void Start()
        {
            _patch = Harmony.CreateAndPatchAll(typeof(FuckRestriction.Patch));
        }

        private void OnDestroy()
        {
            _patch?.UnpatchSelf();
        }
    }
}
