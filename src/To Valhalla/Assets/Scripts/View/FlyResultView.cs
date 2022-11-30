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
            FlightResultHandler.PlayerFlightEnded += OnPlayerFlightEnded;
        }

        private void OnDisable()
        {
            FlightResultHandler.PlayerFlightEnded -= OnPlayerFlightEnded;
        }

        private void OnPlayerFlightEnded(FlightResultData flightData)
        {
            _targetCanvas.enabled = true;

            _heightText.text = flightData.FlyHeight.ToString() + " m";
            _coinsText.text = flightData.FlyCoinsCount.ToString();
            _artifactsText.text = flightData.ArtifactPiecesCount.ToString();
            _keysText.text = flightData.KeysCount.ToString();
        }
    }
}
