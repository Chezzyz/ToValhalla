using Level;
using UnityEngine;

namespace Audio
{
    public class CoinSoundAudioHandler : BaseAudioHandler<CoinSoundAudioSource>
    {
        [Header("Coin Collected Sound")]
        [SerializeField] private AudioClip _coinCollectedSound;
        [SerializeField] [Range(0f,1f)] private float _coinCollectedVolumeScale;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            Coin.VisualCoinCollected += OnCoinCollected;
        }

        private void OnCoinCollected()
        {
            PlayClip(_coinCollectedSound, 1 ,_coinCollectedVolumeScale);
        }
    }
}