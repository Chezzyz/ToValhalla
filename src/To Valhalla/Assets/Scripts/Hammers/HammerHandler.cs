using UnityEngine;

namespace Hammers
{
    public class HammerHandler : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _currentHammerImage; 

        [Header("SerializeField for test")]
        [SerializeField]
        private ScriptableHammerData _currentHammerData;

        private void Start()
        {
            SetHammerSprite(_currentHammerImage, _currentHammerData);
        }

        private void SetHammerSprite(SpriteRenderer currentHammerImage, ScriptableHammerData hammerData)
        {
            currentHammerImage.sprite = hammerData.GetSprite();
        }

        public ScriptableHammerData GetCurrentHummerData() => _currentHammerData;
    }
}
