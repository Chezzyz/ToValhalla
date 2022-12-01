using Input;
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
        }

        private void OnDisable()
        {
            ThrowScalesController.ThrowStarted -= OnThrowStarted;
        }

        private void OnThrowStarted(float arg1, float arg2)
        {
            _playerRenderer.sprite = _currentSkin.GetFlyingSprite();
        }
    }
}
