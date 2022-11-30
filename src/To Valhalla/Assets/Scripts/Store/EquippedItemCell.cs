using Store.View;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class EquippedItemCell : MonoBehaviour
    {
        [SerializeField] private Image _itemSprite;
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _artifactIcon;

        public IStoreItem Item { get; private set; }

        public void Setup(IStoreItem item)
        {
            Item = item;
            _itemSprite.sprite = item.GetSprite();
            SetSelfButton();
        }

        public void SetupDefault()
        {
            Item = null;
            _itemSprite.sprite = _artifactIcon;
        }

        private void SetSelfButton()
        {
            _button.interactable = true;
        }
    }
}