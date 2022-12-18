using Services;
using Services.Settings;
using UnityEngine;

namespace Audio
{
    public class BaseAudioHandler<T> : BaseGameHandler<BaseAudioHandler<T>> where T : MonoBehaviour
    {
        protected AudioSource _audioSource;

        protected virtual void OnEnable()
        {
            SceneLoader.SceneLoaded += OnSceneLoaded;
        }

        protected virtual void OnSceneLoaded(string sceneName)
        {
            _audioSource = FindObjectOfType<T>()?.GetComponent<AudioSource>();
            if (_audioSource == null && sceneName != "MenuScene")
            {
                Debug.LogWarning(typeof(T) + " not found on scene");
            }
        }

        protected void PlayClip(AudioClip clip, float pitch = 1f, float volumeScale = 1f)
        {
            _audioSource.pitch = pitch;
            _audioSource.volume = volumeScale * SettingsHandler.Instance.GetMusicVolume();
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        protected void PlayOneShot(AudioClip clip, float pitch = 1f, float volumeScale = 1f)
        {
            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(clip, volumeScale * SettingsHandler.Instance.GetSoundVolume());
        }
    }
}
