using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class StartSessionHandler : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private List<GameObject> _objectsToDeactivate;

        public static event Action SessionStarted;
        
        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            SessionStarted?.Invoke();
            foreach (var obj in _objectsToDeactivate)
            {
                obj.SetActive(false);
            }
        }
    }
}