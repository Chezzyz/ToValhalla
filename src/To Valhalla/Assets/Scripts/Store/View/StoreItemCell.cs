using System;
using Artifacts;
using Hammers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Store.View
{
    public class StoreItemCell : MonoBehaviour
    {
        [SerializeField] private Image _itemSprite;
        [SerializeField] private Image _cellSprite;
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _hammerIcon;
        [SerializeField] private Sprite _artifactIcon;
        [SerializeField] private Sprite _skinIcon;
        [SerializeField] private Sprite _activeCellSprite;
        [SerializeField] private Sprite _inactiveCellSprite;
        [SerializeField] private Sprite _equippedSprite;

        private SelectedItemWindow _itemWindow;
        private StoreFiller _storeFiller;
        private IStoreItem _item;

        private void Start()
        {
            _itemWindow = FindObjectOfType<SelectedItemWindow>(true);
            _storeFiller = FindObjectOfType<StoreFiller>(true);
        }

        private void OnEnable()
        {
            EquippedItemsHandler.ItemEquipped += OnItemEquipped;
            StoreHandler.ItemBought += OnItemBought;
            EquippedItemsHandler.ArtifactUnequipped += OnArtifactUnequipped;
        }

        private void OnItemBought(IStoreItem item)
        {
            _cellSprite.sprite = GetCorrectCellSprite(_item);
        }

        private void OnArtifactUnequipped(IStoreItem artifact)
        {
            _cellSprite.sprite = GetCorrectCellSprite(_item);
        }

        private void OnItemEquipped(IStoreItem equipped)
        {
            _cellSprite.sprite = GetCorrectCellSprite(_item);
        }

        private Sprite GetCorrectCellSprite(IStoreItem item)
        { 
            return item is null ? _inactiveCellSprite : 
                item.IsEquipped() ? _equippedSprite :
                item.IsBought() ? _activeCellSprite : _inactiveCellSprite;
        } 

        public void Fill(IStoreItem item)
        {
            _item = item;
            _cellSprite.sprite = GetCorrectCellSprite(_item);
            _itemSprite.sprite = item.GetSprite();

            SetSelfButton();
        }

        public void Clear()
        {
            _item = null;
            _itemSprite.sprite = _storeFiller.CurrentSection switch
            {
                StoreItemType.Hammer => _hammerIcon,
                StoreItemType.Artifact => _artifactIcon,
                StoreItemType.Skin => _skinIcon,
                _ => throw new NotImplementedException()
            };
            _button.interactable = false;
        }

        private void SetSelfButton()
        {
            _button.interactable = true;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() =>
            {
                _itemWindow.gameObject.SetActive(true);
                _itemWindow.SetupWindow(_item);
            });
        }

        private void OnDisable()
        {
            EquippedItemsHandler.ItemEquipped -= OnItemEquipped;
            EquippedItemsHandler.ArtifactUnequipped -= OnArtifactUnequipped;
        }
    }
}