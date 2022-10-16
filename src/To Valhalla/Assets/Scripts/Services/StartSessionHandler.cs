using System;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class StartSessionHandler : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        public static event Action SessionStarted;
        
        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            SessionStarted?.Invoke();
            _startButton.gameObject.SetActive(false);
        }
    }
}