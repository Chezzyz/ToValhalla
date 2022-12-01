using Store;
using TMPro;
using UnityEngine;

namespace View
{
    public class CoinsHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            CurrencyHandler.CoinsCountChanged += OnCoinsCountChanged;
        }
        
        private void Start()
        {
            _text.text = CurrencyHandler.Instance.CoinsCount.ToString();
        }

        private void OnCoinsCountChanged(int count)
        {
            _text.text = count.ToString();
        }

        private void OnDisable()
        {
            CurrencyHandler.CoinsCountChanged -= OnCoinsCountChanged;
        }
    }
}