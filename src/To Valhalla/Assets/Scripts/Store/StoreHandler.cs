using System;
using Store.View;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreHandler : MonoBehaviour
    {
        [Header("Section Buttons")] [SerializeField]
        private Button _hammersSectionButton;

        [SerializeField] private Button _artifactsSectionButton;
        [SerializeField] private Button _skinsSectionButton;

        public static event Action<StoreItemType> StoreSectionButtonPressed;
        public static event Action<IStoreItem> ItemBought;

        private void OnEnable()
        {
            SelectedItemWindow.ItemTryToBought += TryBuyItem;
            SelectedItemWindow.ItemTryToEquip += EquipItem;
            SelectedItemWindow.ArtifactsTryToEquip += EquipArtifact;
        }

        private void Start()
        {
            AddEventsToSectionButtons();
        }

        public void OpenStoreAtSection(int index)
        {
            switch (index)
            {
                case 0:
                    OpenSection(StoreItemType.Hammer);
                    break;
                case 1:
                    OpenSection(StoreItemType.Artifact);
                    break;
                case 2:
                    OpenSection(StoreItemType.Skin);
                    break;
                default: throw new NotImplementedException();
            }
        }

        private void OpenSection(StoreItemType itemType)
        {
            StoreSectionButtonPressed?.Invoke(itemType);
        }
        

        private void EquipArtifact(IStoreItem item, int cellIndex)
        {
            EquippedItemsHandler.Instance.EquipArtifact(item, cellIndex);
        }

        private void EquipItem(IStoreItem item)
        {
            EquippedItemsHandler.Instance.EquipItem(item);
        }

        private void TryBuyItem(IStoreItem item)
        {
            if (item.CanBuy())
            {
                CurrencyHandler.Instance.ChangeCoins(-item.GetCoinCost());
                CurrencyHandler.Instance.ChangeArtifactPiece(-item.GetArtifactPiecesCost());
                item.Buy();
                ItemBought?.Invoke(item);
            }
        }

        private void AddEventsToSectionButtons()
        {
            _hammersSectionButton.onClick.AddListener(() => StoreSectionButtonPressed?.Invoke(StoreItemType.Hammer));
            _artifactsSectionButton.onClick.AddListener(() =>
                StoreSectionButtonPressed?.Invoke(StoreItemType.Artifact));
            _skinsSectionButton.onClick.AddListener(() => StoreSectionButtonPressed?.Invoke(StoreItemType.Skin));
        }

        private void OnDisable()
        {
            SelectedItemWindow.ItemTryToBought -= TryBuyItem;
            SelectedItemWindow.ItemTryToEquip -= EquipItem;
            SelectedItemWindow.ArtifactsTryToEquip -= EquipArtifact;
        }
    }
}