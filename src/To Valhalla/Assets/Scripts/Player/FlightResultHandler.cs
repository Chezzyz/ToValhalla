using Player.Throws;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Store;

namespace Player
{
    public class FlightResultHandler : MonoBehaviour
    {
        public static event Action<FlightResultData> PlayerFlightEnded;

        private FlightDataHandler _flightData;

        private void Start()
        {
            _flightData = FindObjectOfType<FlightDataHandler>();
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
            FlightResultData newFlyData =
                new (_flightData.GetCurrentFlightTime(),
                    _flightData.GetCurrentMaxFlightHeight(),
                    _flightData.FlightCoins,
                    _flightData.FlightArtifactPieces,
                    0);

            PlayerFlightEnded?.Invoke(newFlyData);
        }
    }

    public class FlightResultData
    {
        public readonly float FlyTime;
        public readonly int FlyHeight;
        public readonly int FlyCoinsCount;
        public readonly int ArtifactPiecesCount;
        public readonly int KeysCount;

        public FlightResultData(float flyTime, int flyHeight, int flyCoinsCount, int artifactPiecesCount, int keysCount)
        {
            FlyTime = flyTime;
            FlyHeight = flyHeight;
            FlyCoinsCount = flyCoinsCount;
            ArtifactPiecesCount = artifactPiecesCount;
            KeysCount = keysCount;
        }
    }
}