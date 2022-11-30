using Player.Throws;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class FlyResultHandler : MonoBehaviour
    {
        public static event Action<FlyResultData> PlayerFlightEnded;

        private Player _player;

        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        private void OnEnable()
        {
            BaseThrow.ThrowCompleted += OnThrowCompleted;
        }

        private void OnDisable()
        {
            BaseThrow.ThrowCompleted -= OnThrowCompleted;
        }

        private void OnThrowCompleted()
        {
            FlyResultData newFlyData = 
                new FlyResultData(_player.GetCurrentFlightTime(), 
                _player.GetCurrentMaxFlightHeight(),
                _player.GetCurrentCoins(), 
                _player.GetCurrentArtifactParts(), 
                0);

            PlayerFlightEnded?.Invoke(newFlyData);
        }
    }

    public class FlyResultData
    {
        public float flyTime;
        public int flyHeight;
        public int flyCoinsCount;
        public int artifactsCount;
        public int keysCount;

        public FlyResultData(float flyTime, int flyHeight, int flyCoinsCount, int artifactsCount, int keysCount)
        {
            this.flyTime = flyTime;
            this.flyHeight = flyHeight;
            this.flyCoinsCount = flyCoinsCount;
            this.artifactsCount = artifactsCount;
            this.keysCount = keysCount;
        }
    }
}