using UnityEngine;

namespace Hummers
{
    public class HummerHandler : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _currentHummerImage; 

        [Header("SerializeField for test")]
        [SerializeField]
        private ScriptableHummerData _currentHummerData;

        private void Start()
        {
            //Need to load actual hummer
            SetHummerSprite(_currentHummerImage, _currentHummerData);
        }

        private void SetHummerSprite(SpriteRenderer currentHummerImage, ScriptableHummerData hummerData)
        {
            currentHummerImage.sprite = hummerData.GetHummerSprite();
        }

        public ScriptableHummerData GetCurrentHummerData() => _currentHummerData;
    }
}
