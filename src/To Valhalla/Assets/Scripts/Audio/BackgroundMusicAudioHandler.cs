using System;
using Input;
using Level;
using Player.Throws;
using Services;
using Services.SaveLoad;
using Services.Settings;
using UnityEngine;

namespace Audio
{
    public class BackgroundMusicAudioHandler : BaseAudioHandler<BackgroundMusicAudioSource>
    {
        [Header("Menu Music")]
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] [Range(0f,1f)] private float _menuMusicVolumeScale;
        [Header("Fly Preparing Music")]
        [SerializeField] private AudioClip _flyPreparingMusic;
        [SerializeField] [Range(0f,1f)] private float _flyPreparingVolumeScale;
        [Header("Fly Music")]
        [SerializeField] private AudioClip _flyMusic;
        [SerializeField] [Range(0f,1f)] private float _flyVolumeScale;

        private float _currentVolume;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            SaveLoadSystem.SaveLoaded += OnSaveLoaded;
            StartSessionHandler.SessionStarted += OnSessionStarted;
            ThrowScalesController.ThrowStarted += OnThrowStarted;
            SettingsHandler.MusicVolumeLevelChanged += OnMusicVolumeChanged;
        }
        
        private void OnMusicVolumeChanged(float value)
        {
            SetVolume(value);
        }

        private void SetVolume(float value)
        {
            if (_audioSource != null)
            {
                _audioSource.volume = value * _currentVolume;
            }
        }

        private void OnSaveLoaded()
        {
            PlayClip(_menuMusic, volumeScale:_menuMusicVolumeScale);
            _currentVolume = _menuMusicVolumeScale;
        }

        private void OnThrowStarted(float arg1, float arg2)
        {
            PlayClip(_flyMusic, volumeScale:_flyVolumeScale);
            _currentVolume = _flyVolumeScale;
        }

        private void OnSessionStarted()
        {
            PlayClip(_flyPreparingMusic, pitch:0.3f, volumeScale:_flyPreparingVolumeScale);
            _currentVolume = _flyPreparingVolumeScale;
        }
    }
}