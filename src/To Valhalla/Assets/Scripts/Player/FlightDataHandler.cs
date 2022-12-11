using System;
using System.Collections;
using UnityEngine;
using Level;
using Store;

namespace Player
{
    public class FlightDataHandler : MonoBehaviour
    {
        private PlayerFlightDataCounter _playerFlightDataCounter;

        public int FlightCoins { get; private set; } 
        public int FlightArtifactPieces { get; private set; }

        private void Start()
        {
            _playerFlightDataCounter = new PlayerFlightDataCounter(1f);
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

        public int GetCurrentMaxFlightHeight() => _playerFlightDataCounter.GetCurrentMaxFlightHeight();
        public float GetCurrentFlightTime() => _playerFlightDataCounter.GetCurrentFlightTime();

        private void OnArtifactPieceCollected()
        {
            IncreaseFlightArtifactPieces(1);
        }

        private void OnCoinCollected()
        {
            IncreaseFlightCoins(1 * CurrencyHandler.Instance.CoinValueMultiplier);
        }

        private void OnThrowCompleted()
        {
            StopAllCoroutines();
        }

        private void OnThrowStarted(float arg1, float arg2)
        {
            _playerFlightDataCounter.Reset();
            StartCoroutine(FlightDataSimulation(_playerFlightDataCounter));
        }

        private IEnumerator FlightDataSimulation(PlayerFlightDataCounter playerFlightDataCounter)
        {
            while (true)
            {
                yield return null;
                playerFlightDataCounter.Update(Time.deltaTime, transform);
            }
        }
        private void IncreaseFlightCoins(int value) => FlightCoins += value;
        private void IncreaseFlightArtifactPieces(int value) => FlightArtifactPieces += value;
    }
}