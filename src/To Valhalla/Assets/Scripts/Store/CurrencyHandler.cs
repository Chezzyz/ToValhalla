using System;
using Level;
using Services;
using UnityEngine;

namespace Store
{
    public class CurrencyHandler : BaseGameHandler<CurrencyHandler>
    {
        public int CoinsCount { get; private set; }
        
        public int ArtifactPiecesCount { get; private set; }

        private void OnEnable()
        {
            Coin.CoinCollected += OnCoinCollected;
        }

        public void SubtractCoins(int value)
        {
            CoinsCount -= value;
        }

        public void SubtractArtifactsPieces(int value)
        {
            ArtifactPiecesCount -= value;
        }

        private void OnCoinCollected()
        {
            AddCoins(1);
        }

        private void AddCoins(int value)
        {
            CoinsCount += value;
        }

        private void OnArtifactPieceCollected()
        {
            AddArtifactPiece(1);
        }

        private void AddArtifactPiece(int value)
        {
            ArtifactPiecesCount += value;
        }
        
        private void OnDisable()
        {
            Coin.CoinCollected -= OnCoinCollected;
        }
    }
}