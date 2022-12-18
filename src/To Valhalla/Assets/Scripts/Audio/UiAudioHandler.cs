using System;
using System.Collections.Generic;
using System.Linq;
using Services.SaveLoad;
using Store;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class UiAudioHandler : BaseAudioHandler<UiAudioSource>
    {
        [Header("Button Sound")]
        [SerializeField] private AudioClip _buttonSound;
        [SerializeField] [Range(0f,1f)] private float _buttonVolumeScale;
        [Header("Buy Sound")]
        [SerializeField] private AudioClip _buySound;
        [SerializeField] [Range(0f,1f)] private float _buySoundVolumeScale;
        [Header("Equip Sound")]
        [SerializeField] private AudioClip _equipSound;
        [SerializeField] [Range(0f,1f)] private float _equipSoundVolumeScale;

        private bool _canPlaySounds = false;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            StoreHandler.ItemBought += OnItemBought;
            EquippedItemsHandler.ItemEquipped += OnItemEquipped;
            SaveLoadSystem.SaveLoaded += OnSaveLoaded;
        }

        protected override void OnSceneLoaded(string sceneName)
        {
            base.OnSceneLoaded(sceneName);
            List<Button> buttons = FindObjectsOfType<Button>(true).ToList();
            buttons.ForEach(button => button.onClick.AddListener(OnButtonClicked));
            _canPlaySounds = false;
        }

        private void OnSaveLoaded()
        {
            _canPlaySounds = true;
        }

        private void OnButtonClicked()
        {
            if(!_canPlaySounds) return;
            PlayOneShot(_buttonSound, volumeScale:_buttonVolumeScale);
        }
        
        private void OnItemEquipped(IStoreItem obj)
        {
            if(!_canPlaySounds) return;
            PlayOneShot(_equipSound, volumeScale:_equipSoundVolumeScale);
        }

        private void OnItemBought(IStoreItem obj)
        {
            if(!_canPlaySounds) return;
            PlayOneShot(_buySound, volumeScale:_buySoundVolumeScale);
        }
    }
}