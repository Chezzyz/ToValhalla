using System.Collections.Generic;
using UnityEngine;

namespace OffScreenIndicators
{
    public class OffScreenIndicatorsHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Dictionary<OffScreenTargetObject, TargetIndicator> _targetIndicators = new Dictionary<OffScreenTargetObject, TargetIndicator>();
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameObject _targetIndicatorPrefab;

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
            TargetIndicator indicator = Instantiate(_targetIndicatorPrefab, _canvas.transform).GetComponent<TargetIndicator>();
            indicator.InitTargetIndicator(target.gameObject, _mainCamera, _canvas);
            _targetIndicators.Add(target, indicator);
        }
    }
}