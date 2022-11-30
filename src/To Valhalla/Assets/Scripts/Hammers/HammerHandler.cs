using System;
using Store;
using UnityEngine;

namespace Hammers
{
    public class HammerHandler : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _currentHammerImage; 
        
        [SerializeField]
        private ScriptableHammerData _currentHammerData;

        private void OnEnable()
        {
            EquippedItemsHandler.ItemEquipped += OnItemEquipped;
        }

        private void OnItemEquipped(IStoreItem item)
        {
            if (item is not ScriptableHammerData hammer) return;
            SetHammer(hammer);
        }

        private void Start()
        {
            SetHammerSprite(_currentHammerImage, _currentHammerData);
        }

        private void SetHammer(ScriptableHammerData hammer)
        {
            _currentHammerData = hammer;
            SetHammerSprite(_currentHammerImage, hammer);
        }

        private void SetHammerSprite(SpriteRenderer currentHammerImage, ScriptableHammerData hammerData)
        {
            currentHammerImage.sprite = hammerData.GetSprite();
        }

        public ScriptableHammerData GetCurrentHummerData() => _currentHammerData;
    }
}
