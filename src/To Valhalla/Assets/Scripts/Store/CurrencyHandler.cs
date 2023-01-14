using System;
using Level;
using Services;

namespace Store
{
    public class CurrencyHandler : BaseGameHandler<CurrencyHandler>
    {
        public int CoinsCount { get; private set; }
        public int ArtifactPiecesCount { get; private set; }
        
        public int CoinValueMultiplier { get; private set; } = 1;
        
        public int ArtifactPieceValueMultiplier { get; private set; } = 1;

        public static event Action<int> CoinsCountChanged;
        public static event Action<int> ArtifactPiecesCountChanged;


        private void OnEnable()
        {
            Coin.CoinCollected += OnCoinCollected;
            Coin.VisualCoinCollected += OnVisualCoinCollected;
            ArtifactPiece.ArtifactPieceCollected += OnArtifactPieceCollected;
        }

        private void OnVisualCoinCollected()
        {
            CoinsCountChanged?.Invoke(CoinsCount);
        }

        private void OnCoinCollected()
        {
            ChangeCoins(1 * CoinValueMultiplier);
        }

        public void ChangeCoins(int value)
        {
            SetCoins(CoinsCount + value);
        }

        public void SetCoins(int value)
        {
            CoinsCount = value;
        }

        private void OnArtifactPieceCollected()
        {
            ChangeArtifactPieces(1 * ArtifactPieceValueMultiplier);
        }

        public void ChangeArtifactPieces(int value)
        {
            SetArtifactPieces(ArtifactPiecesCount + value);
        }

        public void SetArtifactPieces(int value)
        {
            ArtifactPiecesCount = value;
            ArtifactPiecesCountChanged?.Invoke(ArtifactPiecesCount);
        }
        
        public void SetCoinValueMultiplier(int value) => CoinValueMultiplier = value;

        public void SetArtifactPieceValueMultiplier(int value) => ArtifactPieceValueMultiplier = value;

        
        private void OnDisable()
        {
            Coin.CoinCollected -= OnCoinCollected;
            Coin.VisualCoinCollected -= OnVisualCoinCollected;
            ArtifactPiece.ArtifactPieceCollected -= OnArtifactPieceCollected;
        }
    }
}