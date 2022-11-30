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
                new (_player.GetCurrentFlightTime(),
                    _player.GetCurrentMaxFlightHeight(),
                    _player.GetCurrentCoins(),
                    0,
                    0);

            PlayerFlightEnded?.Invoke(newFlyData);
        }
    }

    public class FlyResultData
    {
        public readonly float FlyTime;
        public readonly int FlyHeight;
        public readonly int FlyCoinsCount;
        public readonly int ArtifactsCount;
        public readonly int KeysCount;

        public FlyResultData(float flyTime, int flyHeight, int flyCoinsCount, int artifactsCount, int keysCount)
        {
            FlyTime = flyTime;
            FlyHeight = flyHeight;
            FlyCoinsCount = flyCoinsCount;
            ArtifactsCount = artifactsCount;
            KeysCount = keysCount;
        }
    }
}