using Player;
using TMPro;
using UnityEngine;

namespace View
{
    public class FlyResultView : MonoBehaviour
    {
        [Header("UI Parts")]
        [SerializeField] private Canvas _targetCanvas;
        [SerializeField] private TextMeshProUGUI _heightText;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _artifactsText;
        [SerializeField] private TextMeshProUGUI _keysText;

        private void OnEnable()
        {
            FlyResultHandler.PlayerFlightEnded += OnPlayerFlightEnded;
        }

        private void OnDisable()
        {
            FlyResultHandler.PlayerFlightEnded -= OnPlayerFlightEnded;
        }

        private void OnPlayerFlightEnded(FlyResultData flightData)
        {
            _targetCanvas.enabled = true;

            _heightText.text = flightData.flyHeight.ToString() + " m";
            _coinsText.text = flightData.flyCoinsCount.ToString();
            _artifactsText.text = flightData.artifactsCount.ToString();
            _keysText.text = flightData.keysCount.ToString();
        }
    }
}
