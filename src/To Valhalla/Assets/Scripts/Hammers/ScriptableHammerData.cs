using System.Collections.Generic;
using Store;
using UnityEngine;

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
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _cost;
        [Header("Model")] [SerializeField] private float _weight;
        [SerializeField] private HammerType _hammerType;
        [SerializeField] private List<float> _scalePartsPercents = new List<float>();

        private bool _isBought;

        #region StoreItem
        public string GetName() => _name;
        public string GetDescription() => _description;
        public int GetCoinCost() => _cost;
        public int GetArtifactPiecesCost() => 0;
        public Sprite GetSprite() => _sprite;
        public void Buy() => _isBought = true;
        public bool CanBuy() => CurrencyHandler.Instance.CoinsCount >= _cost;
        public bool IsBought() => _isBought;
        public StoreItemType GetStoreItemType() => StoreItemType.Hammer;
        #endregion

        public float GetWeight() => _weight;
        public HammerType GetHammerType() => _hammerType;
        public IReadOnlyCollection<float> GetScalePartsInPercent() => _scalePartsPercents;
    }
}