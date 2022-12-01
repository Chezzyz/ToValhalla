using System.Collections.Generic;
using UnityEngine;
using Services;
using System;

namespace OffScreenIndicators
{
    public class OffScreenIndicatorsHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _targetCanvas;
        [SerializeField] private Dictionary<OffScreenTargetObject, TargetIndicator> _targetIndicators = new Dictionary<OffScreenTargetObject, TargetIndicator>();
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameObject _targetIndicatorPrefab;

        private void OnEnable()
        {
            StartSessionHandler.SessionStarted += OnSessionStarted;
            _targetCanvas.enabled = false;
        }

        private void OnDisable()
        {
            StartSessionHandler.SessionStarted -= OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            _targetCanvas.enabled = true;
        }

        private void Update()
        {
            if(_targetIndicators.Count > 0)
            {
                foreach (var indicator in _targetIndicators)
                {
                    indicator.Value.UpdateTargetIndicator();
                }
            }
        }

        public void RemoveTargetIndicator(OffScreenTargetObject target)
        {
            var indicator = _targetIndicators[target];
            _targetIndicators.Remove(target);
            Destroy(indicator.gameObject);
        }

        public void AddTargetIndicator(OffScreenTargetObject target)
        {
            TargetIndicator indicator = Instantiate(_targetIndicatorPrefab, _targetCanvas.transform).GetComponent<TargetIndicator>();
            indicator.InitTargetIndicator(target.gameObject, _mainCamera, _targetCanvas, target.GetSpriteToShow());
            _targetIndicators.Add(target, indicator);
        }
    }
}