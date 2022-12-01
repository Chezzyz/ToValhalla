using System;
using Level;
using Services;

namespace Store
{
    public class CurrencyHandler : BaseGameHandler<CurrencyHandler>
    {
        public int CoinsCount { get; private set; } = 100;
        public int ArtifactPiecesCount { get; private set; } = 100;
        
        public int CoinValueMultiplier { get; private set; } = 1;
        
        public int ArtifactPieceValueMultiplier { get; private set; } = 1;

        public static event Action<int> CoinsCountChanged;
        public static event Action<int> ArtifactPiecesCountChanged;


        private void OnEnable()
        {
            Coin.CoinCollected += OnCoinCollected;
            ArtifactPiece.ArtifactPieceCollected += OnArtifactPieceCollected;
        }

        private void OnCoinCollected()
        {
            ChangeCoins(1 * CoinValueMultiplier);
        }

        public void ChangeCoins(int value)
        {
            CoinsCount += value;
            CoinsCountChanged?.Invoke(CoinsCount);
        }

        private void OnArtifactPieceCollected()
        {
            ChangeArtifactPiece(1 * ArtifactPieceValueMultiplier);
        }

        public void ChangeArtifactPiece(int value)
        {
            ArtifactPiecesCount += value;
            ArtifactPiecesCountChanged?.Invoke(ArtifactPiecesCount);
        }
        
        public void SetCoinValueMultiplier(int value) => CoinValueMultiplier = value;

        public void SecArtifactPieceValueMultiplier(int value) => ArtifactPieceValueMultiplier = value;

        
        private void OnDisable()
        {
            Coin.CoinCollected -= OnCoinCollected;
            ArtifactPiece.ArtifactPieceCollected -= OnArtifactPieceCollected;
        }
    }
}