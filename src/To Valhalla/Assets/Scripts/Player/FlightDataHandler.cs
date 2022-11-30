using System;
using System.Collections;
using UnityEngine;
using Level;

namespace Player
{
    public class FlightDataHandler : MonoBehaviour
    {
        private PlayerFlightData _playerFlightData;

        public int FlightCoins { get; private set; } 
        public int FlightArtifactPieces { get; private set; }

        public int CoinValueMultiplier { get; private set; } = 1;

        private void Start()
        {
            _playerFlightData = new PlayerFlightData(1f);
        }

        private void OnEnable()
        {
            Input.ThrowScalesController.ThrowStarted += OnThrowStarted;
            Throws.BaseThrow.ThrowCompleted += OnThrowCompleted;
            Coin.CoinCollected += OnCoinCollected;
            ArtifactPiece.ArtifactPieceCollected += OnArtifactPieceCollected;
        }

        private void OnDisable()
        {
            Input.ThrowScalesController.ThrowStarted -= OnThrowStarted;
            Throws.BaseThrow.ThrowCompleted -= OnThrowCompleted;
            Coin.CoinCollected -= OnCoinCollected;
            ArtifactPiece.ArtifactPieceCollected -= OnArtifactPieceCollected;
        }

        public int GetCurrentMaxFlightHeight() => _playerFlightData.GetCurrentMaxFlightHeight();
        public float GetCurrentFlightTime() => _playerFlightData.GetCurrentFlightTime();
        public void SetCoinValueMultiplier(int value) => CoinValueMultiplier = value;

        private void OnArtifactPieceCollected()
        {
            IncreaseFlightArtifactPieces(1);
        }

        private void OnCoinCollected()
        {
            IncreaseFlightCoins(1 * CoinValueMultiplier);
        }

        private void OnThrowCompleted()
        {
            StopAllCoroutines();
        }

        private void OnThrowStarted(float arg1, float arg2)
        {
            _playerFlightData.Reset();
            StartCoroutine(FlightDataSimulation(_playerFlightData));
        }

        private IEnumerator FlightDataSimulation(PlayerFlightData playerFlightData)
        {
            while (true)
            {
                yield return null;
                playerFlightData.Update(Time.deltaTime, transform);
            }
        }
        private void IncreaseFlightCoins(int value) => FlightCoins += value;
        private void IncreaseFlightArtifactPieces(int value) => FlightArtifactPieces += value;
    }
}