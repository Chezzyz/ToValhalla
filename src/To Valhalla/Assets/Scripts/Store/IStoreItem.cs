using Artifacts;
using Hammers;
using Player;
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
        
        // public bool IsEquipped()
        // {
        //     return GetStoreItemType() == StoreItemType.Hammer &&
        //            EquippedItemsHandler.Instance.GetHammer() == (ScriptableHammerData)this
        //            || GetStoreItemType() == StoreItemType.Artifact &&
        //            (EquippedItemsHandler.Instance.GetFirstArtifact() == (ScriptableArtifactData)this ||
        //             EquippedItemsHandler.Instance.GetSecondArtifact() == (ScriptableArtifactData)this)
        //            || GetStoreItemType() == StoreItemType.Skin &&
        //            EquippedItemsHandler.Instance.EquippedSkin == (ScriptableSkinData)this;
        // }

        bool IsEquipped();

        void Buy();

        void Reset();

        bool CanBuy();
    }

    public enum StoreItemType
    {
        Hammer, Artifact, Skin
    }
}