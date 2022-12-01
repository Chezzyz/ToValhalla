using System;
using System.Linq;
using Artifacts;
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
        [Header("Artifact Buttons")]
        [SerializeField] private Button _firstArtifactButton;
        [SerializeField] private Button _secondArtifactButton;
        [SerializeField] private Button _unselectArtifactButton;
        [SerializeField] private Sprite _artifactIcon;

        public static event Action<IStoreItem> ItemTryToBought;
        public static event Action<IStoreItem> ItemTryToEquip;
        public static event Action<IStoreItem, int> ArtifactsTryToEquip;

        private IStoreItem _currentItem;

        private void OnEnable()
        {
            StoreHandler.ItemBought += OnItemBought;
            EquippedItemsHandler.ItemEquipped += OnItemEquipped;
        }

        private void OnItemEquipped(IStoreItem obj)
        {
            if (_currentItem.GetStoreItemType() is StoreItemType.Hammer or StoreItemType.Skin) _button.interactable = false;
            if (_currentItem.GetStoreItemType() is StoreItemType.Artifact) SetupArtifactsEquipButtons(obj);
        }

        private void OnItemBought(IStoreItem item)
        {
            if (_currentItem == item)
            {
                if(item.GetStoreItemType() is StoreItemType.Artifact) SetupArtifactsEquipButtons(item);
                else SetupButtonAsEquip(item);
            }
        }

        public void SetupWindow(IStoreItem item)
        {
            _name.text = item.GetName();
            _description.text = item.GetDescription();
            _itemSprite.sprite = item.GetSprite();
            _currentItem = item;
            SetupButtons(item);
            
            if (item.GetStoreItemType() is StoreItemType.Hammer) SetupScales(item as ScriptableHammerData);
            else _scaleView.gameObject.SetActive(false);
        }

        private void SetupScales(ScriptableHammerData hammerData)
        {
            _scaleView.gameObject.SetActive(true);
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
            _unselectArtifactButton.onClick.AddListener(() =>
            {
                ArtifactsTryToEquip?.Invoke(item, -1);
                SetupArtifactsEquipButtons(item);
            });
        }

        private void SetActiveArtifactsButtons(bool state)
        {
            _button.gameObject.SetActive(!state);

            _firstArtifactButton.gameObject.SetActive(state);
            _firstArtifactButton.GetComponentInChildren<ButtonIcon>(true).GetComponent<Image>().sprite
                = EquippedItemsHandler.Instance.GetFirstArtifact() == null
                    ? _artifactIcon
                    : EquippedItemsHandler.Instance.GetFirstArtifact().GetSprite();

            _secondArtifactButton.gameObject.SetActive(state);
            _secondArtifactButton.GetComponentInChildren<ButtonIcon>(true).GetComponent<Image>().sprite
                = EquippedItemsHandler.Instance.GetSecondArtifact() == null
                    ? _artifactIcon
                    : EquippedItemsHandler.Instance.GetSecondArtifact().GetSprite();

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
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ItemTryToBought?.Invoke(item));
        }

        private void SetupButtonAsEquip(IStoreItem item)
        {
            SetActiveArtifactsButtons(false);
            _button.interactable = !item.IsEquipped();
            _buttonEquipText.enabled = true;
            _costText.enabled = false;
            _currencySprite.enabled = false;
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ItemTryToEquip?.Invoke(item));
        }

        private void OnDisable()
        {
            StoreHandler.ItemBought -= OnItemBought;
            EquippedItemsHandler.ItemEquipped -= OnItemEquipped;
        }
    }
}