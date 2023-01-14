using System;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Settings
{
    public class SettingsHandler : BaseGameHandler<SettingsHandler>
    {
        public static event Action<float> MusicVolumeLevelChanged;

        private Slider _musicSlider;
        private Slider _soundSlider;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }

        private void OnEnable()
        {
            SceneLoader.SceneLoaded += OnSceneLoaded;
        }

        public float GetMusicVolume() => _musicSlider.value;
        public void SetMusicVolume(float value) => _musicSlider.value = value;
        public float GetSoundVolume() => _soundSlider.value;
        public void SetSoundVolume(float value) => _soundSlider.value = value;

        private void OnSceneLoaded(string _)
        {
            AddSliders();
        }

        private void AddSliders()
        {
            _musicSlider = FindObjectOfType<MusicSlider>().GetComponent<Slider>();
            _musicSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.AddListener(OnMusicVolumeLevelChanged);
            _soundSlider = FindObjectOfType<SoundSlider>().GetComponent<Slider>();
        }


        private void OnMusicVolumeLevelChanged(float value)
        {
            MusicVolumeLevelChanged?.Invoke(value);
        }
    }
}