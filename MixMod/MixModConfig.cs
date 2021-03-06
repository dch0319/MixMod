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
					devEnabledEntry = _config.Bind("Dev", "DevEnabled", defaultValue: false, "?????????? ???????????????????????? ????????");
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
					isInternalEntry = _config.Bind("Dev", "IsInternal", defaultValue: false, "?????????? ???????????????????????? ????????");
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
					boardEntry = _config.Bind("Dev", "Board", 0, "?????????? ???????????????? ???????? ?????? ????????????");
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
					goldenCoinEntry = _config.Bind("Dev", "GoldenCoin", CardState.Default, "?????????????????? ?????? ??????????????");
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
					testShortcutEntry = _config.Bind("??????", "TestShortcut", new KeyboardShortcut(KeyCode.U, KeyCode.LeftControl), "?????????????? ?????? ????????????");
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
					devEnabledEntry = config.Bind("Dev", "DevEnabled", defaultValue: false, "?????????? ???????????????????????? ????????");
				}
				if (dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "Dev" && x.Key.Key == "IsInternal"))
				{
					isInternalEntry = config.Bind("Dev", "IsInternal", defaultValue: false, "?????????? ???????????????????????? ????????");
				}
				if (dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "Dev" && x.Key.Key == "Board"))
				{
					boardEntry = config.Bind("Dev", "Board", 0, "????????????????????????");
				}
				if (dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "Dev" && x.Key.Key == "GoldenCoin"))
				{
					goldenCoinEntry = config.Bind("Dev", "GoldenCoin", CardState.Default, "???????????????");
				}
			}
			enableShortcutsEntry = config.Bind("??????", "????????????", defaultValue: false, "????????????");
			timeScaleEnabledEntry = config.Bind("??????", "??????????????????", defaultValue: false, "??????????????????");
			timeScaleEntry = config.Bind("??????", "????????????", 1f, new ConfigDescription("?????????????????????", new AcceptableValueRange<float>(1f, 8f)));
			skipHeroIntroEntry = config.Bind("????????????", "??????????????????", defaultValue: false, "??????????????????");
			shutUpBobEntry = config.Bind("????????????", "???????????????", defaultValue: false, "???????????????");
			extendedBMEntry = config.Bind("????????????", "???????????????", defaultValue: false, "???????????????");
			disableRandomForEmotesEntry = config.Bind("????????????", "DisableRandomForEmotes", defaultValue: false, "?????????????????????????????????????????????????????????????????????");
			emoteSpamBlockerEntry = config.Bind("????????????", "EmoteSpamBlocker", defaultValue: false, "?????????????????????????????????");
			emotesBeforeBlockEntry = config.Bind("????????????", "EmotesBeforeBlock", 0, "????????? 10 ????????????????????????????????? ????????????????????????????????????????????????\n ?????????????????????????????????");
			disableThinkEmotesEntry = config.Bind("????????????", "??????????????????", defaultValue: false, "???????????????????????????");
			goldenEntry = config.Bind("????????????", "?????????", CardState.Default, "????????????");
			diamondEntry = config.Bind("????????????", "????????????", CardState.Default, "???????????????");
			showOpponentRankInGameEntry = config.Bind("????????????", "??????????????????", defaultValue: false, "?????????????????????????????????");
			quickPackOpeningEnabledEntry = config.Bind("??????", "????????????", defaultValue: false, "??????????????????????????????????????????");
			moveEnemyCardsEntry = config.Bind("??????", "MoveEnemyCards", defaultValue: false, "??????????????????????????????????????????");
			firesideGatheringEntry = config.Bind("??????", "??????????????????", defaultValue: false, "?????????????????????GPS??????");
			latitudeEntry = config.Bind("??????", "??????", 0.0, "??????");
			longitudeEntry = config.Bind("??????", "??????", 0.0, "??????");
			gpsAccuracyEntry = config.Bind("??????", "Gps????????????", 54.0, "????????????");
			if (dictionary != null && dictionary.Any((KeyValuePair<ConfigDefinition, string> x) => x.Key.Section == "??????" && x.Key.Key == "????????????"))
			{
				testShortcutEntry = config.Bind("??????", "????????????", new KeyboardShortcut(KeyCode.U, KeyCode.LeftControl), "???????????????");
			}
			soundMuteShortcutEntry = config.Bind("??????", "??????", new KeyboardShortcut(KeyCode.V));
			resetTimeScaleShortcutEntry = config.Bind("??????", "??????????????????", new KeyboardShortcut(KeyCode.LeftArrow));
			maxTimeScaleShortcutEntry = config.Bind("??????", "??????????????????", new KeyboardShortcut(KeyCode.RightArrow));
			doubleTimeScaleShortcutEntry = config.Bind("??????", "2???????????????", new KeyboardShortcut(KeyCode.UpArrow));
			devideTimeScaleShortcutEntry = config.Bind("??????", "0.5???????????????", new KeyboardShortcut(KeyCode.DownArrow));
			concedeShortcutEntry = config.Bind("??????", "??????", new KeyboardShortcut(KeyCode.Space, KeyCode.LeftControl));
			continueMulliganShortcutEntry = config.Bind("??????", "ContinueMulligan", new KeyboardShortcut(KeyCode.Space));
			squelchShortcutEntry = config.Bind("??????", "Squelch", new KeyboardShortcut(KeyCode.C));
			shutUpBobShortcutEntry = config.Bind("??????", "???????????????", new KeyboardShortcut(KeyCode.B));
			endTurnShortcutEntry = config.Bind("??????", "????????????", new KeyboardShortcut(KeyCode.Space));
			greetingsEmoteShortcutEntry = config.Bind("??????", "?????????", new KeyboardShortcut(KeyCode.Z));
			wellPlayedEmoteShortcutEntry = config.Bind("??????", "????????????", new KeyboardShortcut(KeyCode.A));
			thanksEmoteShortcutEntry = config.Bind("??????", "??????", new KeyboardShortcut(KeyCode.Q));
			wowEmoteShortcutEntry = config.Bind("??????", "??????", new KeyboardShortcut(KeyCode.W));
			oopsEmoteShortcutEntry = config.Bind("??????", "??????", new KeyboardShortcut(KeyCode.S));
			threatenEmoteShortcutEntry = config.Bind("??????", "??????", new KeyboardShortcut(KeyCode.X));
			copyBattleTagShortcutEntry = config.Bind("??????", "????????????ID", new KeyboardShortcut(KeyCode.C, KeyCode.LeftControl));
			copySelectedBattleTagShortcutEntry = config.Bind("??????", "?????????????????????ID", new KeyboardShortcut(KeyCode.Mouse0));
			simulateDisconnectShortcutEntry = config.Bind("??????", "??????????????????", new KeyboardShortcut(KeyCode.D, KeyCode.LeftControl));
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
