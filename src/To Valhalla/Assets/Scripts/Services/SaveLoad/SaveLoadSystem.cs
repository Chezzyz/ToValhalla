using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Player;
using Store;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadSystem : BaseGameHandler<SaveLoadSystem>
    {
        [SerializeField]
        private GameStateMap _gameStateMap;

        public static event Action SaveLoaded;

        private bool _isLoaded;

        private void OnEnable()
        {
            FlightResultHandler.PlayerFlightEnded += OnFlightEnded;
            StoreHandler.ItemBought += OnItemBought;
            EquippedItemsHandler.ItemEquipped += OnItemEquipped;
        }

        private void OnItemEquipped(IStoreItem obj)
        {
            if(_isLoaded) Save();
        }

        private void OnItemBought(IStoreItem obj)
        {
            if(_isLoaded) Save();
        }

        private void OnFlightEnded(FlightResultData obj)
        {
            Save();
        }

        private void Start()
        {
            Load();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Save()
        {
            SaveInts();
            SaveDoubles();
            SaveBools();
            SaveStrings();
            PlayerPrefs.Save();
        }

        #region Save
        private void SaveInts()
        {
            Dictionary<string, Func<int>> intValuesMap = _gameStateMap.GetIntGameValuesMap();

            intValuesMap
                .Keys
                .ToList()
                .ForEach(alias => SaveInt(alias, intValuesMap[alias].Invoke()));
        }

        private void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            Debug.Log($"Key {key} with value {value} saved");
        }

        private void SaveDoubles()
        {
            Dictionary<string, Func<double>> floatValuesMap = _gameStateMap.GetFloatGameValuesMap();

            floatValuesMap
                .Keys
                .ToList()
                .ForEach(alias => SaveDouble(alias, floatValuesMap[alias].Invoke()));
        }

        private void SaveDouble(string key, double value)
        {
            PlayerPrefs.SetFloat(key, (float)value);
            Debug.Log($"Key {key} with value {value} saved");
        }

        private void SaveBools()
        {
            Dictionary<string, Func<bool>> boolValuesMap = _gameStateMap.GetBoolGameValuesMap();

            boolValuesMap
                .Keys
                .ToList()
                .ForEach(alias => SaveBool(alias, boolValuesMap[alias].Invoke() ? 1 : 0));
        }

        private void SaveBool(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            Debug.Log($"Key {key} with value {value} saved");
        }

        private void SaveStrings()
        {
            Dictionary<string, Func<string>> stringValuesMap = _gameStateMap.GetStringGameValuesMap();

            stringValuesMap
                .Keys
                .ToList()
                .ForEach(alias => SaveString(alias, stringValuesMap[alias].Invoke()));
        }

        private void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            Debug.Log($"Key {key} with value {value} saved");
        }
        #endregion

        private void Load()
        {
            LoadInts();
            LoadFloats();
            LoadBools();
            LoadStrings();
            SaveLoaded?.Invoke();
            _isLoaded = true;
        }

        #region Load
        private void LoadInts()
        {
            Dictionary<string, Action<int>> intMap = _gameStateMap.GetIntGameValueSettersMap();

            intMap
                .Keys
                .ToList()
                .ForEach(alias => LoadInt(alias, intMap[alias]));
        }

        private void LoadInt(string key, Action<int> action)
        {
            if (PlayerPrefs.HasKey(key))
            {
                int value = PlayerPrefs.GetInt(key);
                action.Invoke(value);
                Debug.Log($"Key {key} loaded with value {value}");
            } 
            else
            {
                Debug.LogWarning($"Key {key} not found in PlayerPrefs");
            }
        }

        private void LoadFloats()
        {
            Dictionary<string, Action<double>> floatMap = _gameStateMap.GetFloatGameValueSettersMap();

            floatMap
                .Keys
                .ToList()
                .ForEach(alias => LoadFloat(alias, floatMap[alias]));
        }

        private void LoadFloat(string key, Action<double> action)
        {
            if (PlayerPrefs.HasKey(key))
            {
                float value = PlayerPrefs.GetFloat(key);
                action.Invoke(value);
                Debug.Log($"Key {key} loaded with value {value}");
            }
            else
            {
                Debug.LogWarning($"Key {key} not found in PlayerPrefs");
            }
        }

        private void LoadBools()
        {
            Dictionary<string, Action<bool>> boolMap = _gameStateMap.GetBoolGameValueSettersMap();

            boolMap
                .Keys
                .ToList()
                .ForEach(alias => LoadBool(alias, boolMap[alias])); 
        }

        private void LoadBool(string key, Action<bool> action)
        {
            if (PlayerPrefs.HasKey(key))
            {
                bool value = PlayerPrefs.GetInt(key) == 1;
                action.Invoke(value);
                Debug.Log($"Key {key} loaded with value {value}");
            }
            else
            {
                Debug.LogWarning($"Key {key} not found in PlayerPrefs");
            }
        }

        private void LoadStrings()
        {
            Dictionary<string, Action<string>> stringMap = _gameStateMap.GetStringGameValueSettersMap();

            stringMap
                .Keys
                .ToList()
                .ForEach(alias => LoadString(alias, stringMap[alias]));
        }

        private void LoadString(string key, Action<string> action)
        {
            if (PlayerPrefs.HasKey(key))
            {
                string value = PlayerPrefs.GetString(key);
                action.Invoke(value);
                Debug.Log($"Key {key} loaded with value {value}");
            }
            else
            {
                Debug.LogWarning($"Key {key} not found in PlayerPrefs");
            }
        }
        #endregion

        private void OnDisable()
        {
            FlightResultHandler.PlayerFlightEnded -= OnFlightEnded;
            StoreHandler.ItemBought -= OnItemBought;
            EquippedItemsHandler.ItemEquipped -= OnItemEquipped;
        }
        
        [Button]
        public void ResetSaves()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("All values reseted");
        }
    }
}
