using UnityEngine;

namespace Store
{
    public interface IStoreItem
    {
        string GetName();

        string GetDescription();

        int GetCoinCost();
        
        int GetArtifactPiecesCost();

        Sprite GetSprite();

        StoreItemType GetStoreItemType();

        bool IsBought();

        void Buy();

        bool CanBuy();
    }

    public enum StoreItemType
    {
        Hammer, Artifact, Skin
    }
}