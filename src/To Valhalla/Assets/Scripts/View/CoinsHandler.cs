using Level;
using TMPro;
using UnityEngine;
using Player;

namespace View
{
    public class CoinsHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private FlightDataHandler _flightDataHandler;

        private int _count;

        private void OnEnable()
        {
            Coin.CoinCollected += OnCoinCollected;
        }

        private void OnCoinCollected()
        {
            _count += 1 * _flightDataHandler.CoinValueMultiplier;
            _text.text = _count.ToString();
        }

        private void OnDisable()
        {
            Coin.CoinCollected -= OnCoinCollected;
        }
    }
}