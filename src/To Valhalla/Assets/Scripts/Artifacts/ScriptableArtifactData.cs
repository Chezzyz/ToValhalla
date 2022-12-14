using Store;
using System.Collections;
using System.Collections.Generic;
using Artifacts.Effects;
using UnityEngine;
using NaughtyAttributes;

namespace Artifacts
{
    [CreateAssetMenu(fileName = "ArtifactData", menuName = "ScriptableObjects/Artifacts", order = 0)]
    public class ScriptableArtifactData : ScriptableObject, IStoreItem
    {
        [Header("Store")] [SerializeField] private string _name;
        [SerializeField] private string _description;
        [ShowAssetPreview] [SerializeField] private Sprite _sprite;
        [SerializeField] private int _coinsCost;
        [SerializeField] private int _artifactPiecesCost;

        [Header("Effect")] [SerializeField] private ArtifactEffect _artifactEffect;

        private bool _isBought;

        private readonly Dictionary<ArtifactEffect, BaseArtifactEffect> _artifactEffectsMap
            = new()
            {
                { ArtifactEffect.DoubleMoney, new DoubleMoneyEffect() },
                { ArtifactEffect.GreaterMagnet, new GreaterMagnetEffect() },
                { ArtifactEffect.CoinForObstacle, new CoinForObstacleEffect() },
                { ArtifactEffect.InfinitePowerScale, new InfinitePowerScale() },
                { ArtifactEffect.AdditionalVelocityForDuration, new AdditionalVelocityForDuration() }
            };

        #region StoreItem

        public string GetName() => _name;
        public string GetDescription() => _description;
        public int GetCoinCost() => _coinsCost;
        public int GetArtifactPiecesCost() => _artifactPiecesCost;
        public Sprite GetSprite() => _sprite;
        public void Buy() => _isBought = true;
        public void Reset() => _isBought = false;

        public bool CanBuy() => CurrencyHandler.Instance.CoinsCount >= _coinsCost
                                && CurrencyHandler.Instance.ArtifactPiecesCount >= _artifactPiecesCost;

        public bool IsBought() => _isBought;

        public bool IsEquipped() => EquippedItemsHandler.Instance.GetFirstArtifact() == this ||
                                    EquippedItemsHandler.Instance.GetSecondArtifact() == this;

        public StoreItemType GetStoreItemType() => StoreItemType.Artifact;

        #endregion

        public BaseArtifactEffect GetBaseArtifactEffect() => _artifactEffectsMap[_artifactEffect];
    }

    public enum ArtifactEffect
    {
        DoubleMoney,
        GreaterMagnet,
        CoinForObstacle,
        InfinitePowerScale,
        AdditionalVelocityForDuration
    }
}