using Level;
using TMPro;
using UnityEngine;

namespace View
{
    public class CoinsHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private int _count;

        private void OnEnable()
        {
            Coin.CoinCollected += OnCoinCollected;
        }

        private void OnCoinCollected()
        {
            _count++;
            _text.text = _count.ToString();
        }

        private void OnDisable()
        {
            Coin.CoinCollected -= OnCoinCollected;
        }
    }
}