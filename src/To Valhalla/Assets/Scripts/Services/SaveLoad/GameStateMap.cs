using System;
using System.Collections.Generic;
using Network;
using Store;
using UnityEngine;

namespace Services.SaveLoad
{
    [CreateAssetMenu(fileName = "GameStateMap", menuName = "ScriptableObjects/GameStateMap", order = 5)]
    public class GameStateMap : ScriptableObject
    {
        private const string USERNAME_ALIAS = "Username";

        private const string PLAYER_ID_ALIAS = "PlayerId";

        //public static readonly string CURRENT_LEVEL_ALIAS = "CurrentLevel";
        private const string HAMMER_BOUGHT_PREFIX_ALIAS = "Hammer_";
        private const string ARTIFACT_BOUGHT_PREFIX_ALIAS = "Artifact_";
        private const string SKIN_BOUGHT_PREFIX_ALIAS = "Skin_";
        private const string HAMMER_EQUIPPED_ALIAS = "HammerEquipped";
        private const string ARTIFACT_FIRST_EQUIPPED_ALIAS = "ArtifactFirstEquipped";
        private const string ARTIFACT_SECOND_EQUIPPED_ALIAS = "ArtifactSecondEquipped";
        private const string SKIN_EQUIPPED_PREFIX_ALIAS = "SkinEquipped_";
        private const string COINS_COUNT_ALIAS = "CoinsCount";
        private const string ARTIFACT_PIECES_COUNT_ALIAS = "ArtifactPiecesCount";
        private const string GAME_TIME_ALIAS = "GameTime";

        private const string BEST_SCORES_PREFIX_ALIAS = "BestScore_";
        // public static readonly string MUSIC_IS_ON_ALIAS = "MusicIsOn";
        // public static readonly string VOLUME_LEVEL_ALIAS = "VolumeLevel";
        //private static readonly string LANGUAGE_ALIAS = "Language";

        public Dictionary<string, Func<int>> GetIntGameValuesMap()
        {
            return new Dictionary<string, Func<int>>
            {
                { COINS_COUNT_ALIAS, () => CurrencyHandler.Instance.CoinsCount },
                { ARTIFACT_PIECES_COUNT_ALIAS, () => CurrencyHandler.Instance.ArtifactPiecesCount },
                //{ CURRENT_LEVEL_ALIAS, () => "Midguard" },
                //{ VOLUME_LEVEL_ALIAS, () => SettingsHandler.GetVolumeLevel() }
            };
        }

        public Dictionary<string, Action<int>> GetIntGameValueSettersMap()
        {
            return new Dictionary<string, Action<int>>
            {
                { COINS_COUNT_ALIAS, (value) => CurrencyHandler.Instance.ChangeCoins(value) },
                { ARTIFACT_PIECES_COUNT_ALIAS, (value) => CurrencyHandler.Instance.ChangeArtifactPiece(value) },
                //{ VOLUME_LEVEL_ALIAS, (value) => SettingsHandler.Instance.SetVolumeLevel(value) }
            };
        }

        public Dictionary<string, Func<double>> GetFloatGameValuesMap()
        {
            return new Dictionary<string, Func<double>>
            {
                { GAME_TIME_ALIAS, () => GameTimeHandler.Instance.GetGameTime().TotalSeconds }
            };
        }

        public Dictionary<string, Action<double>> GetFloatGameValueSettersMap()
        {
            return new Dictionary<string, Action<double>>
            {
                { GAME_TIME_ALIAS, GameTimeHandler.Instance.SetGameTime }
            };
        }

        public Dictionary<string, Func<bool>> GetBoolGameValuesMap()
        {
            Dictionary<string, Func<bool>> boolsMap = new Dictionary<string, Func<bool>>
            {
                // { MUSIC_IS_ON_ALIAS, () => SettingsHandler.MusicIsOn() }
            };

            AddBoolValuesForStoreItemBought(StoreItemsHandler.Instance.GetHammers(), HAMMER_BOUGHT_PREFIX_ALIAS,
                boolsMap);
            AddBoolValuesForStoreItemBought(StoreItemsHandler.Instance.GetArtifacts(), ARTIFACT_BOUGHT_PREFIX_ALIAS,
                boolsMap);
            AddBoolValuesForStoreItemBought(StoreItemsHandler.Instance.GetSkins(), SKIN_BOUGHT_PREFIX_ALIAS, boolsMap);

            return boolsMap;
        }

        private void AddBoolValuesForStoreItemBought(List<IStoreItem> items, string prefix,
            Dictionary<string, Func<bool>> boolsMap)
        {
            items.ForEach(item => boolsMap.Add($"{prefix}{item.GetName()}", item.IsBought));
        }

        public Dictionary<string, Action<bool>> GetBoolGameValueSettersMap()
        {
            Dictionary<string, Action<bool>> boolsMap = new Dictionary<string, Action<bool>>
            {
                // { MUSIC_IS_ON_ALIAS, (value) => SettingsHandler.Instance.SetMusicIsOn(value) }
            };

            AddBoolValueSettersForStoreItem(StoreItemsHandler.Instance.GetHammers(), HAMMER_BOUGHT_PREFIX_ALIAS,
                boolsMap);
            AddBoolValueSettersForStoreItem(StoreItemsHandler.Instance.GetArtifacts(), ARTIFACT_BOUGHT_PREFIX_ALIAS,
                boolsMap);
            AddBoolValueSettersForStoreItem(StoreItemsHandler.Instance.GetSkins(), SKIN_BOUGHT_PREFIX_ALIAS, boolsMap);
            return boolsMap;
        }

        private void AddBoolValueSettersForStoreItem(List<IStoreItem> items, string prefix,
            Dictionary<string, Action<bool>> boolsMap)
        {
            items.ForEach(item => boolsMap.Add($"{prefix}{item.GetName()}", (state) =>
            {
                if (state) item.Buy();
            }));
        }

        public Dictionary<string, Func<string>> GetStringGameValuesMap()
        {
            return new Dictionary<string, Func<string>>
            {
                { USERNAME_ALIAS, NetworkPlayerHandler.Instance.GetUsername },
                { PLAYER_ID_ALIAS, NetworkPlayerHandler.Instance.GetPlayerId },
                { HAMMER_EQUIPPED_ALIAS, () => EquippedItemsHandler.Instance.GetHammer()?.GetName() },
                { ARTIFACT_FIRST_EQUIPPED_ALIAS, () => EquippedItemsHandler.Instance.GetFirstArtifact()?.GetName() },
                { ARTIFACT_SECOND_EQUIPPED_ALIAS, () => EquippedItemsHandler.Instance.GetSecondArtifact()?.GetName() }
                // { LANGUAGE_ALIAS, () => LanguagePicker.Instance.GetLocale() }
                // {SKIN_EQUIPPED_PREFIX_ALIAS, }
            };
        }

        public Dictionary<string, Action<string>> GetStringGameValueSettersMap()
        {
            return new Dictionary<string, Action<string>>
            {
                { USERNAME_ALIAS, NetworkPlayerHandler.Instance.SetUsername },
                { PLAYER_ID_ALIAS, NetworkPlayerHandler.Instance.SetPlayerId },
                {
                    HAMMER_EQUIPPED_ALIAS,
                    (hammerName) =>
                        EquippedItemsHandler.Instance.EquipItem(StoreItemsHandler.Instance.GetHammerByName(hammerName))
                },
                {
                    ARTIFACT_FIRST_EQUIPPED_ALIAS,
                    (artifactName) =>
                        EquippedItemsHandler.Instance.EquipArtifact(
                            StoreItemsHandler.Instance.GetArtifactByName(artifactName),
                            0)
                },
                {
                    ARTIFACT_SECOND_EQUIPPED_ALIAS,
                    (artifactName) =>
                        EquippedItemsHandler.Instance.EquipArtifact(
                            StoreItemsHandler.Instance.GetArtifactByName(artifactName),
                            1)
                },

                // { LANGUAGE_ALIAS, (value) => LanguagePicker.Instance.SetLocaleFromSave(value) }
            };
        }
    }
}