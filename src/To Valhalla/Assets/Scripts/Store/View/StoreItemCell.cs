using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Store.View
{
    public class StoreItemCell : MonoBehaviour
    {
        [SerializeField] private Image _itemSprite;
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _hammerIcon;
        [SerializeField] private Sprite _artifactIcon;
        [SerializeField] private Sprite _skinIcon;

        private SelectedItemWindow _itemWindow;
        private StoreFiller _storeFiller;
        private IStoreItem _item;

        private void Start()
        {
            _itemWindow = FindObjectOfType<SelectedItemWindow>(true);
            _storeFiller = FindObjectOfType<StoreFiller>(true);
        }

        public void Fill(IStoreItem item)
        {
            _item = item;
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
            _button.onClick.AddListener(() =>
            {
                _itemWindow.gameObject.SetActive(true);
                _itemWindow.SetupWindow(_item);
            });
        }
    }
}