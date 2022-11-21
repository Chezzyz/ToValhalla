using Player.Throws;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerFlyResult : MonoBehaviour
    {
        public event Action<FlyResultData> PlayerFlightEnded;

        [SerializeField] private Player _player;

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
                0, 
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