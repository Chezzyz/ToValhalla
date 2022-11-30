using System;
using System.Linq;
using Hammers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Store.View
{
    public class SelectedItemWindow : MonoBehaviour
    {
        [Header("Item")] [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _itemSprite;
        [SerializeField] private CirclePowerScaleView _scaleView;
        [Header("Button")] [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonEquipText;
        [SerializeField] private Image _currencySprite;
        [SerializeField] private TMP_Text _costText;

        [Header("Artifact Buttons")] [SerializeField]
        private EquippedItemsHandler _itemsHandler;

        [SerializeField] private Button _firstArtifactButton;
        [SerializeField] private Button _secondArtifactButton;
        [SerializeField] private Button _unselectArtifactButton;
        [SerializeField] private Sprite _artifactIcon;
        [SerializeField] private Sprite _coinSprite;

        public static event Action<IStoreItem> ItemTryToBought;
        public static event Action<IStoreItem> ItemTryToEquip;
        public static event Action<IStoreItem, int> ArtifactsTryToEquip;

        private IStoreItem _currentItem;

        private void OnEnable()
        {
            StoreHandler.ItemBought += OnItemBought;
        }

        private void OnItemBought(IStoreItem item)
        {
            if (_currentItem == item)
            {
                SetupButtonAsEquip(item);
            }
        }

        public void SetupWindow(IStoreItem item)
        {
            _name.text = item.GetName();
            _description.text = item.GetDescription();
            _itemSprite.sprite = item.GetSprite();
            _currentItem = item;
            SetupButtons(item);
            if (item.GetStoreItemType() is StoreItemType.Hammer)
                SetupScales(item as ScriptableHammerData);
        }

        private void SetupScales(ScriptableHammerData hammerData)
        {
            _scaleView.SetScaleSectors(Image.Origin360.Top, 0f, hammerData.GetScalePartsInPercent().ToList());
        }

        private void SetupButtons(IStoreItem item)
        {
            if (item.IsBought())
            {
                if (item.GetStoreItemType() is StoreItemType.Hammer or StoreItemType.Skin) SetupButtonAsEquip(item);
                if (item.GetStoreItemType() is StoreItemType.Artifact) SetupArtifactsEquipButtons(item);
            }
            else
            {
                SetupButtonAsBuy(item);
            }
        }

        private void SetupArtifactsEquipButtons(IStoreItem item)
        {
            SetActiveArtifactsButtons(true);
            _firstArtifactButton.onClick.AddListener(() => ArtifactsTryToEquip?.Invoke(item, 0));
            _secondArtifactButton.onClick.AddListener(() => ArtifactsTryToEquip?.Invoke(item, 1));
            _unselectArtifactButton.onClick.AddListener(() => ArtifactsTryToEquip?.Invoke(item, -1));
        }

        private void SetActiveArtifactsButtons(bool state)
        {
            _button.gameObject.SetActive(!state);

            _firstArtifactButton.gameObject.SetActive(state);
            _firstArtifactButton.GetComponentInChildren<Image>(true).sprite
                = _itemsHandler.GetFirstArtifact() == null
                    ? _artifactIcon
                    : _itemsHandler.GetFirstArtifact().GetSprite();

            _secondArtifactButton.gameObject.SetActive(state);
            _secondArtifactButton.GetComponentInChildren<Image>(true).sprite
                = _itemsHandler.GetSecondArtifact() == null
                    ? _artifactIcon
                    : _itemsHandler.GetSecondArtifact().GetSprite();

            _unselectArtifactButton.gameObject.SetActive(state);
        }

        private void SetupButtonAsBuy(IStoreItem item)
        {
            SetActiveArtifactsButtons(false);
            _buttonEquipText.enabled = false;
            _costText.enabled = true;
            _currencySprite.enabled = true;

            _costText.text = item.GetCoinCost().ToString();
            _button.interactable = item.CanBuy();
            _button.onClick.AddListener(() => ItemTryToBought?.Invoke(item));
        }

        private void SetupButtonAsEquip(IStoreItem item)
        {
            SetActiveArtifactsButtons(false);
            _button.interactable = true;
            _buttonEquipText.enabled = true;
            _costText.enabled = false;
            _currencySprite.enabled = false;

            _button.onClick.AddListener(() => ItemTryToEquip?.Invoke(item));
        }

        private void OnDisable()
        {
            StoreHandler.ItemBought -= OnItemBought;
        }
    }
}