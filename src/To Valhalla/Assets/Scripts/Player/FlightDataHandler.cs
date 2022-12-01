using System;
using System.Collections;
using UnityEngine;
using Level;
using Store;

namespace Player
{
    public class FlightDataHandler : MonoBehaviour
    {
        private PlayerFlightData _playerFlightData;

        public int FlightCoins { get; private set; } 
        public int FlightArtifactPieces { get; private set; }

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