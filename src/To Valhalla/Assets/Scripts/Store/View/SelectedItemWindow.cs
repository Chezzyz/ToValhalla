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
        [SerializeField] private Image _coinSprite;
        [SerializeField] private TMP_Text _costText;

        [Header("Artifact Buttons")] [SerializeField]
        private Button _firstArtifactButton;

        [SerializeField] private Button _secondArtifactButton;
        [SerializeField] private Button _unselectArtifactButton;
        [SerializeField] private Sprite _artifactIcon;
        [SerializeField] private TMP_Text _artifactCoinCostText;
        [SerializeField] private TMP_Text _artifactPiecesCostText;
        [SerializeField] private Image _artifactCoinSprite;
        [SerializeField] private Image _artifactPiecesSprite;

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
            if (_currentItem.GetStoreItemType() is StoreItemType.Hammer or StoreItemType.Skin)
                _button.interactable = false;
            if (_currentItem.GetStoreItemType() is StoreItemType.Artifact) SetupArtifactsEquipButtons(obj);
        }

        private void OnItemBought(IStoreItem item)
        {
            if (_currentItem == item)
            {
                if (item.GetStoreItemType() is StoreItemType.Artifact) SetupArtifactsEquipButtons(item);
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
            if (item.GetStoreItemType() is StoreItemType.Hammer or StoreItemType.Skin)
            {
                if (item.IsBought()) SetupButtonAsEquip(item);
                else SetupButtonAsBuy(item);
            }
            if (item.GetStoreItemType() is StoreItemType.Artifact)
            {
                if (item.IsBought()) SetupArtifactsEquipButtons(item);
                else SetupArtifactBuyButton(item);
            }
        }

        private void SetupArtifactsEquipButtons(IStoreItem item)
        {
            SetActiveArtifactsButtons(true);
            _firstArtifactButton.onClick.RemoveAllListeners();
            _firstArtifactButton.onClick.AddListener(() => ArtifactsTryToEquip?.Invoke(item, 0));
            _secondArtifactButton.onClick.RemoveAllListeners();
            _secondArtifactButton.onClick.AddListener(() => ArtifactsTryToEquip?.Invoke(item, 1));
            _unselectArtifactButton.onClick.RemoveAllListeners();
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
           SetBuyButtonView(true, item);
            _costText.text = item.GetCoinCost().ToString();

            SetBuyButtonOn(item);
        }

        private void SetupArtifactBuyButton(IStoreItem item)
        {
            SetBuyButtonView(true, item);
            _artifactCoinCostText.text = ((ScriptableArtifactData)item).GetCoinCost().ToString();
            _artifactPiecesCostText.text = ((ScriptableArtifactData)item).GetArtifactPiecesCost().ToString();
            
            SetBuyButtonOn(item);
        }

        //true = artifact view, else = default view
        private void SetBuyButtonView(bool state, IStoreItem item)
        {
            SetActiveArtifactsButtons(false);
            _buttonEquipText.enabled = false;
            
            _coinSprite.enabled = state && item.GetStoreItemType() != StoreItemType.Artifact;
            _costText.enabled = state && item.GetStoreItemType() != StoreItemType.Artifact;

            _artifactCoinSprite.enabled = state && item.GetStoreItemType() == StoreItemType.Artifact;
            _artifactPiecesSprite.enabled = state && item.GetStoreItemType() == StoreItemType.Artifact;
            _artifactCoinCostText.enabled = state && item.GetStoreItemType() == StoreItemType.Artifact;
            _artifactPiecesCostText.enabled = state && item.GetStoreItemType() == StoreItemType.Artifact;
        }

        private void SetBuyButtonOn(IStoreItem item)
        {
            _button.interactable = item.CanBuy();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ItemTryToBought?.Invoke(item));
        }

        private void SetupButtonAsEquip(IStoreItem item)
        {
            SetBuyButtonView(false, item);
            SetActiveArtifactsButtons(false);
            _button.interactable = !item.IsEquipped();
            _buttonEquipText.enabled = true;

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