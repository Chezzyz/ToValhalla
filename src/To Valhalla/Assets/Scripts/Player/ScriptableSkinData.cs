using NaughtyAttributes;
using Store;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/Skin", order = 0)]
    public class ScriptableSkinData : ScriptableObject, IStoreItem
    {
        [Header("Store")] 
        [SerializeField] private string _skinName;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _coinsCost;
        [Header("View")]
        [ShowAssetPreview]
        [SerializeField] private Sprite _idleSprite;
        [ShowAssetPreview]
        [SerializeField] private Sprite _flyingSprite;

        private bool _isBought;
        
        #region StoreItem
        public string GetName() => _skinName;
        public string GetDescription() => _description;
        public int GetCoinCost() => _coinsCost;
        public int GetArtifactPiecesCost() => 0;
        public Sprite GetSprite() => _sprite;
        public void Buy() => _isBought = true;
        public void Reset() => _isBought = false;
        public bool CanBuy() => CurrencyHandler.Instance.CoinsCount >= _coinsCost;
        public bool IsBought() => _isBought;
        public StoreItemType GetStoreItemType() => StoreItemType.Artifact;
        #endregion
        
        public Sprite GetIdleSprite() => _idleSprite; 
        public Sprite GetFlyingSprite() => _flyingSprite; 
    }
}