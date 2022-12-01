using Input;
using Store;
using UnityEngine;

namespace Player
{
    public class VikingVisual : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _playerRenderer;
        [SerializeField] private ScriptableSkinData _currentSkin;

        private void OnEnable()
        {
            ThrowScalesController.ThrowStarted += OnThrowStarted;
            EquippedItemsHandler.ItemEquipped += OnItemEquipped;
        }

        private void OnThrowStarted(float arg1, float arg2)
        {
            _playerRenderer.sprite = _currentSkin.GetFlyingSprite();
        }
        
        private void OnItemEquipped(IStoreItem item)
        {
            if(item is not ScriptableSkinData skin) return;
            SetCurrentSkin(skin);
        }

        private void SetCurrentSkin(ScriptableSkinData skin)
        {
            _currentSkin = skin;
            _playerRenderer.sprite = _currentSkin.GetIdleSprite();
        }
        
        private void OnDisable()
        {
            ThrowScalesController.ThrowStarted -= OnThrowStarted;
            EquippedItemsHandler.ItemEquipped -= OnItemEquipped;
        }
    }
}
