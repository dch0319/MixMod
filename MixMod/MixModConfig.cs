using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx.Configuration;
using UnityEngine;

namespace MixMod
{
	public class MixModConfig
	{
		private static MixModConfig _mixModConfig;

		private static PropertyInfo orphanedEntriesInfo = typeof(ConfigFile).GetProperty("OrphanedEntries", BindingFlags.Instance | BindingFlags.NonPublic);

		private readonly ConfigFile _config;

		internal ConfigEntry<bool> devEnabledEntry;

		internal ConfigEntry<bool> isInternalEntry;

		internal ConfigEntry<int> boardEntry;

		internal ConfigEntry<CardState> goldenCoinEntry;

		internal ConfigEntry<bool> enableShortcutsEntry;

		internal ConfigEntry<bool> timeScaleEnabledEntry;

		internal ConfigEntry<float> timeScaleEntry;

		internal ConfigEntry<bool> skipHeroIntroEntry;

		internal ConfigEntry<bool> shutUpBobEntry;

		internal ConfigEntry<bool> extendedBMEntry;

		internal ConfigEntry<bool> disableRandomForEmotesEntry;

		internal ConfigEntry<bool> useExtendedEmotesEntry;

		internal ConfigEntry<bool> emoteSpamBlockerEntry;

		internal ConfigEntry<int> emotesBeforeBlockEntry;

		internal ConfigEntry<bool> disableThinkEmotesEntry;

		internal ConfigEntry<CardState> goldenEntry;

		internal ConfigEntry<CardState> diamondEntry;

		internal ConfigEntry<bool> showOpponentRankInGameEntry;

		internal ConfigEntry<bool> quickPackOpeningEnabledEntry;

		internal ConfigEntry<bool> moveEnemyCardsEntry;

		internal ConfigEntry<bool> firesideGatheringEntry;

		internal ConfigEntry<double> latitudeEntry;

		internal ConfigEntry<double> longitudeEntry;

		internal ConfigEntry<double> gpsAccuracyEntry;

		internal ConfigEntry<bool> isDeviceInfoModifiedEntry;

		internal ConfigEntry<OSCategory> osEntry;

		internal ConfigEntry<ScreenCategory> screenEntry;

		internal ConfigEntry<string> deviceNameEntry;

		internal ConfigEntry<string> operatingSystemEntry;

		internal ConfigEntry<KeyboardShortcut> testShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> soundMuteShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> resetTimeScaleShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> maxTimeScaleShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> doubleTimeScaleShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> devideTimeScaleShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> concedeShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> continueMulliganShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> squelchShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> shutUpBobShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> endTurnShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> greetingsEmoteShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> wellPlayedEmoteShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> thanksEmoteShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> wowEmoteShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> oopsEmoteShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> threatenEmoteShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> copyBattleTagShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> copySelectedBattleTagShortcutEntry;

		internal ConfigEntry<KeyboardShortcut> simulateDisconnectShortcutEntry;

		public bool DevEnabled
		{
			get
			{
				return devEnabledEntry?.Value ?? false;
			}
			set
			{
				if (devEnabledEntry == null)
				{
					devEnabledEntry = _config.Bind("Dev", "DevEnabled", defaultValue: false, "Режим разработчика мода");
				}
				devEnabledEntry.Value = value;
			}
		}

		public bool IsInternal
		{
			get
			{
				return isInternalEntry?.Value ?? false;
			}
			set
			{
				if (isInternalEntry == null)
				{
					isInternalEntry = _config.Bind("Dev", "IsInternal", defaultValue: false, "Режим разработчика игры");
				}
				isInternalEntry.Value = value;
			}
		}

		public int Board
		{
			get
			{
				return boardEntry?.Value ?? 0;
			}
			set
			{
				if (boardEntry == null)
				{
					boardEntry = _config.Bind("Dev", "Board", 0, "Номер игрового поля для замены");
				}
				boardEntry.Value = value;
			}
		}

		public CardState GoldenCoin
		{
			get
			{
				return goldenCoinEntry?.Value ?? CardState.Default;
			}
			set
			{
				if (goldenCoinEntry == null)
				{
					goldenCoinEntry = _config.Bind("Dev", "GoldenCoin", CardState.Default, "Изменения для монеток");
				}
				goldenCoinEntry.Value = value;
			}
		}

		public bool EnableShortcuts
		{
			get
			{
				return enableShortcutsEntry.Value;
			}
			set
			{
				enableShortcutsEntry.Value = value;
			}
		}

		public bool TimeScaleEnabled
		{
			get
			{
				return timeScaleEnabledEntry.Value;
			}
			set
			{
				timeScaleEnabledEntry.Value = value;
			}
		}

		public float TimeScale
		{
			get
			{
				return timeScaleEntry.Value;
			}
			set
			{
				timeScaleEntry.Value = value;
			}
		}

		public bool SkipHeroIntro
		{
			get
			{
				return skipHeroIntroEntry.Value;
			}
			set
			{
				skipHeroIntroEntry.Value = value;
			}
		}

		public bool ShutUpBob
		{
			get
			{
				return shutUpBobEntry.Value;
			}
			set
			{
				shutUpBobEntry.Value = value;
			}
		}

		public bool ExtendedBM
		{
			get
			{
				return extendedBMEntry.Value;
			}
			set
			{
				extendedBMEntry.Value = value;
			}
		}

		public bool DisableRandomForEmotes
		{
			get
			{
				return disableRandomForEmotesEntry.Value;
			}
			set
			{
				disableRandomForEmotesEntry.Value = value;
			}
		}

		public bool UseExtendedEmotes
		{
			get
			{
				return useExtendedEmotesEntry.Value;
			}
			set
			{
				useExtendedEmotesEntry.Value = value;
			}
		}

		public bool EmoteSpamBlocker
		{
			get
			{
				return emoteSpamBlockerEntry.Value;
			}
			set
			{
				emoteSpamBlockerEntry.Value = value;
			}
		}

		public int EmotesBeforeBlock
		{
			get
			{
				return emotesBeforeBlockEntry.Value;
			}
			set
			{
				emotesBeforeBlockEntry.Value = value;
			}
		}

		public bool DisableThinkEmotes
		{
			get
			{
				return disableThinkEmotesEntry.Value;
			}
			set
			{
				disableThinkEmotesEntry.Value = value;
			}
		}

		public CardState GOLDEN
		{
			get
			{
				return goldenEntry.Value;
			}
			set
			{
				goldenEntry.Value = value;
			}
		}

		public CardState DIAMOND
		{
			get
			{
				return diamondEntry.Value;
			}
			set
			{
				diamondEntry.Value = value;
			}
		}

		public bool ShowOpponentRankInGame
		{
			get
			{
				return showOpponentRankInGameEntry.Value;
			}
			set
			{
				showOpponentRankInGameEntry.Value = value;
			}
		}

		public bool QuickPackOpeningEnabled
		{
			get
			{
				return quickPackOpeningEnabledEntry.Value;
			}
			set
			{
				quickPackOpeningEnabledEntry.Value = value;
			}
		}

		public bool MoveEnemyCards
		{
			get
			{
				return moveEnemyCardsEntry.Value;
			}
			set
			{
				moveEnemyCardsEntry.Value = value;
			}
		}

		public bool FiresideGathering
		{
			get
			{
				return firesideGatheringEntry.Value;
			}
			set
			{
				firesideGatheringEntry.Value = value;
			}
		}

		public double Latitude
		{
			get
			{
				return latitudeEntry.Value;
			}
			set
			{
				latitudeEntry.Value = value;
			}
		}

		public double Longitude
		{
			get
			{
				return longitudeEntry.Value;
			}
			set
			{
				longitudeEntry.Value = value;
			}
		}

		public double GpsAccuracy
		{
			get
			{
				return gpsAccuracyEntry.Value;
			}
			set
			{
				gpsAccuracyEntry.Value = value;
			}
		}

		public bool IsDeviceInfoModified
		{
			get
			{
				return isDeviceInfoModifiedEntry.Value;
			}
			set
			{
				isDeviceInfoModifiedEntry.Value = value;
			}
		}

		public OSCategory Os
		{
			get
			{
				return osEntry.Value;
			}
			set
			{
				osEntry.Value = value;
			}
		}

		public ScreenCategory Screen
		{
			get
			{
				return screenEntry.Value;
			}
			set
			{
				screenEntry.Value = value;
			}
		}

		public string DeviceName
		{
			get
			{
				return deviceNameEntry.Value;
			}
			set
			{
				deviceNameEntry.Value = value;
			}
		}

		public string OperatingSystem
		{
			get
			{
				return operatingSystemEntry.Value;
			}
			set
			{
				operatingSystemEntry.Value = value;
			}
		}

		public KeyboardShortcut TestShortcut
		{
			get
			{
				return testShortcutEntry?.Value ?? KeyboardShortcut.Empty;
			}
			set
			{
				if (testShortcutEntry == null)
				{
					testShortcutEntry = _config.Bind("捷径", "TestShortcut", new KeyboardShortcut(KeyCode.U, KeyCode.LeftControl), "Клавиша для тестов");
				}
				testShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut SoundMuteShortcut
		{
			get
			{
				return soundMuteShortcutEntry.Value;
			}
			set
			{
				soundMuteShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut ResetTimeScaleShortcut
		{
			get
			{
				return resetTimeScaleShortcutEntry.Value;
			}
			set
			{
				resetTimeScaleShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut MaxTimeScaleShortcut
		{
			get
			{
				return maxTimeScaleShortcutEntry.Value;
			}
			set
			{
				maxTimeScaleShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut DoubleTimeScaleShortcut
		{
			get
			{
				return doubleTimeScaleShortcutEntry.Value;
			}
			set
			{
				doubleTimeScaleShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut DevideTimeScaleShortcut
		{
			get
			{
				return devideTimeScaleShortcutEntry.Value;
			}
			set
			{
				devideTimeScaleShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut ConcedeShortcut
		{
			get
			{
				return concedeShortcutEntry.Value;
			}
			set
			{
				concedeShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut ContinueMulliganShortcut
		{
			get
			{
				return continueMulliganShortcutEntry.Value;
			}
			set
			{
				continueMulliganShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut SquelchShortcut
		{
			get
			{
				return squelchShortcutEntry.Value;
			}
			set
			{
				squelchShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut ShutUpBobShortcut
		{
			get
			{
				return shutUpBobShortcutEntry.Value;
			}
			set
			{
				shutUpBobShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut EndTurnShortcut
		{
			get
			{
				return endTurnShortcutEntry.Value;
			}
			set
			{
				endTurnShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut GreetingsEmoteShortcut
		{
			get
			{
				return greetingsEmoteShortcutEntry.Value;
			}
			set
			{
				greetingsEmoteShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut WellPlayedEmoteShortcut
		{
			get
			{
				return wellPlayedEmoteShortcutEntry.Value;
			}
			set
			{
				wellPlayedEmoteShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut ThanksEmoteShortcut
		{
			get
			{
				return thanksEmoteShortcutEntry.Value;
			}
			set
			{
				thanksEmoteShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut WowEmoteShortcut
		{
			get
			{
				return wowEmoteShortcutEntry.Value;
			}
			set
			{
				wowEmoteShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut OopsEmoteShortcut
		{
			get
			{
				return oopsEmoteShortcutEntry.Value;
			}
			set
			{
				oopsEmoteShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut ThreatenEmoteShortcut
		{
			get
			{
				return threatenEmoteShortcutEntry.Value;
			}
			set
			{
				threatenEmoteShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut CopyBattleTagShortcut
		{
			get
			{
				return copyBattleTagShortcutEntry.Value;
			}
			set
			{
				copyBattleTagShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut CopySelectedBattleTagShortcut
		{
			get
			{
				return copySelectedBattleTagShortcutEntry.Value;
			}
			set
			{
				copySelectedBattleTagShortcutEntry.Value = value;
			}
		}

		public KeyboardShortcut SimulateDisconnectShortcut
		{
			get
			{
				return simulateDisconnectShortcutEntry.Value;
			}
			set
			{
				simulateDisconnectShortcutEntry.Value = value;
			}
		}

		public MixModConfig(ConfigFile config)
		{
			Dictionary<ConfigDefinition, string> dictionary = orphanedEntriesInfo?.GetValue(config) as Dictionary<ConfigDefinition, string>;
			if (dictionary != null)
			{
				if (dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "Dev" && x.Key.Key == "DevEnabled"))
				{
					devEnabledEntry = config.Bind("Dev", "DevEnabled", defaultValue: false, "Режим разработчика мода");
				}
				if (dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "Dev" && x.Key.Key == "IsInternal"))
				{
					isInternalEntry = config.Bind("Dev", "IsInternal", defaultValue: false, "Режим разработчика игры");
				}
				if (dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "Dev" && x.Key.Key == "Board"))
				{
					boardEntry = config.Bind("Dev", "Board", 0, "比赛场地号码替换");
				}
				if (dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "Dev" && x.Key.Key == "GoldenCoin"))
				{
					goldenCoinEntry = config.Bind("Dev", "GoldenCoin", CardState.Default, "硬币的变化");
				}
			}
			enableShortcutsEntry = config.Bind("全局", "启用热键", defaultValue: false, "启用热键");
			timeScaleEnabledEntry = config.Bind("全局", "启用动画加速", defaultValue: false, "启用动画加速");
			timeScaleEntry = config.Bind("全局", "动画速度", 1f, new ConfigDescription("加速动画的价值", new AcceptableValueRange<float>(1f, 8f)));
			skipHeroIntroEntry = config.Bind("游戏对局", "跳过英雄介绍", defaultValue: false, "跳过英雄介绍");
			shutUpBobEntry = config.Bind("游戏对局", "鲍勃闭嘴！", defaultValue: false, "鲍勃闭嘴！");
			extendedBMEntry = config.Bind("游戏对局", "表情无冷却", defaultValue: false, "表情无冷却");
			disableRandomForEmotesEntry = config.Bind("游戏对局", "DisableRandomForEmotes", defaultValue: false, "如果所有目标都是随机选择的，则禁用使用随机表情");
			emoteSpamBlockerEntry = config.Bind("游戏对局", "EmoteSpamBlocker", defaultValue: false, "启用表情垃圾邮件拦截器");
			emotesBeforeBlockEntry = config.Bind("游戏对局", "EmotesBeforeBlock", 0, "对手在 10 秒内可以说出的情绪量。 如果超过此数字，他的表情将被禁用\n 以在比赛开始时禁用表情");
			disableThinkEmotesEntry = config.Bind("游戏对局", "禁用英雄想法", defaultValue: false, "禁用表情英雄的想法");
			goldenEntry = config.Bind("游戏对局", "全金卡", CardState.Default, "金卡变更");
			diamondEntry = config.Bind("游戏对局", "全钻石卡", CardState.Default, "钻石卡变更");
			showOpponentRankInGameEntry = config.Bind("游戏对局", "显示对手排名", defaultValue: false, "启用显示当前对手的排名");
			quickPackOpeningEnabledEntry = config.Bind("其他", "快速开包", defaultValue: false, "通过按空格键可以快速打开卡包");
			moveEnemyCardsEntry = config.Bind("其他", "MoveEnemyCards", defaultValue: false, "在旁观模式中展示对手手中的牌");
			firesideGatheringEntry = config.Bind("礼物", "云上炉边聚会", defaultValue: false, "为炉边聚会开启GPS仿真");
			latitudeEntry = config.Bind("礼物", "纬度", 0.0, "纬度");
			longitudeEntry = config.Bind("礼物", "经度", 0.0, "经度");
			gpsAccuracyEntry = config.Bind("礼物", "Gps定位精度", 54.0, "定位精度");
			if (dictionary != null && dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "捷径" && x.Key.Key == "测试捷径"))
			{
				testShortcutEntry = config.Bind("捷径", "测试捷径", new KeyboardShortcut(KeyCode.U, KeyCode.LeftControl), "测试的关键");
			}
			soundMuteShortcutEntry = config.Bind("捷径", "静音", new KeyboardShortcut(KeyCode.V));
			resetTimeScaleShortcutEntry = config.Bind("捷径", "重置动画速度", new KeyboardShortcut(KeyCode.LeftArrow));
			maxTimeScaleShortcutEntry = config.Bind("捷径", "最大动画速度", new KeyboardShortcut(KeyCode.RightArrow));
			doubleTimeScaleShortcutEntry = config.Bind("捷径", "2倍动画速度", new KeyboardShortcut(KeyCode.UpArrow));
			devideTimeScaleShortcutEntry = config.Bind("捷径", "0.5倍动画速度", new KeyboardShortcut(KeyCode.DownArrow));
			concedeShortcutEntry = config.Bind("捷径", "投降", new KeyboardShortcut(KeyCode.Space, KeyCode.LeftControl));
			continueMulliganShortcutEntry = config.Bind("捷径", "ContinueMulligan", new KeyboardShortcut(KeyCode.Space));
			squelchShortcutEntry = config.Bind("捷径", "Squelch", new KeyboardShortcut(KeyCode.C));
			shutUpBobShortcutEntry = config.Bind("捷径", "鲍勃闭嘴！", new KeyboardShortcut(KeyCode.B));
			endTurnShortcutEntry = config.Bind("捷径", "结束回合", new KeyboardShortcut(KeyCode.Space));
			greetingsEmoteShortcutEntry = config.Bind("捷径", "打招呼", new KeyboardShortcut(KeyCode.Z));
			wellPlayedEmoteShortcutEntry = config.Bind("捷径", "打得不错", new KeyboardShortcut(KeyCode.A));
			thanksEmoteShortcutEntry = config.Bind("捷径", "乞讨", new KeyboardShortcut(KeyCode.Q));
			wowEmoteShortcutEntry = config.Bind("捷径", "哇哦", new KeyboardShortcut(KeyCode.W));
			oopsEmoteShortcutEntry = config.Bind("捷径", "失误", new KeyboardShortcut(KeyCode.S));
			threatenEmoteShortcutEntry = config.Bind("捷径", "威胁", new KeyboardShortcut(KeyCode.X));
			copyBattleTagShortcutEntry = config.Bind("捷径", "复制战网ID", new KeyboardShortcut(KeyCode.C, KeyCode.LeftControl));
			copySelectedBattleTagShortcutEntry = config.Bind("捷径", "复制选中的战网ID", new KeyboardShortcut(KeyCode.Mouse0));
			simulateDisconnectShortcutEntry = config.Bind("捷径", "模拟断开连接", new KeyboardShortcut(KeyCode.D, KeyCode.LeftControl));
		}

		public static void Load(ConfigFile config)
		{
			_mixModConfig = new MixModConfig(config);
		}

		public static MixModConfig Get()
		{
			return _mixModConfig;
		}
	}
}
