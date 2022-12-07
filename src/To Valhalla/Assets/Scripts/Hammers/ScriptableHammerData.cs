using System;
using System.Collections.Generic;
using Store;
using UnityEngine;
using NaughtyAttributes;

namespace Hammers
{
    public enum HammerType
    {
        Light,
        Medium,
        Heavy
    }

    [CreateAssetMenu(fileName = "HammerData", menuName = "ScriptableObjects/Hammers", order = 0)]
    public class ScriptableHammerData : ScriptableObject, IStoreItem
    {
        [Header("Store")] [SerializeField] private string _name;
        [SerializeField] private string _description;
        [ShowAssetPreview]
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _cost;
        [Header("Model")] [SerializeField] private float _weight;
        [SerializeField] private HammerType _hammerType;
        [SerializeField] private List<float> _scalePartsPercents = new();
        [SerializeField] private List<float> _scalePartsMultipliers = new();

        private bool _isBought;

        #region StoreItem
        public string GetName() => _name;
        public string GetDescription() => _description;
        public int GetCoinCost() => _cost;
        public int GetArtifactPiecesCost() => 0;
        public Sprite GetSprite() => _sprite;
        public void Buy() => _isBought = true;
        public void Reset() => _isBought = false;
        public bool CanBuy() => CurrencyHandler.Instance.CoinsCount >= _cost;
        public bool IsBought() => _isBought;
        public bool IsEquipped() => EquippedItemsHandler.Instance.GetHammer() == this;
        public StoreItemType GetStoreItemType() => StoreItemType.Hammer;
        #endregion

        public float GetWeight() => _weight;
        public HammerType GetHammerType() => _hammerType;
        public IReadOnlyCollection<float> GetScalePartsInPercent() => _scalePartsPercents;

        public float GetPowerMultiplier(float power)
        {
            float lowZone = _scalePartsPercents[0];
            float lowZoneMultiplier = _scalePartsMultipliers[0];
            float mediumZone = _scalePartsPercents[1] + lowZone;
            float mediumZoneMultiplier = _scalePartsMultipliers[1];
            float highZone = _scalePartsPercents[2] + mediumZone;
            float highZoneMultiplier = _scalePartsMultipliers[2];
            float overZoneMultiplier = _scalePartsMultipliers[3];

            return power < lowZone ? lowZoneMultiplier
                : power < mediumZone ? mediumZoneMultiplier
                : power < highZone ? highZoneMultiplier
                : overZoneMultiplier;
        }
    }
}