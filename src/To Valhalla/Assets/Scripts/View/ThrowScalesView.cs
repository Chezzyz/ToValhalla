using System;
using UnityEngine;
using static Input.ThrowScalesController;

namespace View
{
    public class ThrowScalesView : MonoBehaviour
    {
        [SerializeField] private Transform _directionArrow;
        [SerializeField] private RectTransform _powerScalePivot;
        [SerializeField] private Canvas _scalesParent;

        private float _originZRotation;

        private void OnEnable()
        {
            DirectionScaleChanged += OnDirectionScaleChanged;
            PowerScaleChanged += OnPowerScaleChanged;
            ThrowStarted += OnThrowStarted;

            _originZRotation = _powerScalePivot.localEulerAngles.z;
        }

        private void OnThrowStarted(float _, float _2)
        {
            _scalesParent.enabled = false;
        }

        private void OnDirectionScaleChanged(float value)
        {
            ChangeDirectionScale(value);
        }
        
        private void OnPowerScaleChanged(float value)
        {
            ChangePowerScale(value);
        }

        private void ChangePowerScale(float value)
        {
            float newZ = Mathf.Lerp(_originZRotation, _originZRotation - 360f, value / 100);
            _powerScalePivot.eulerAngles = new Vector3(0f, 0f, newZ);
        }

        private void ChangeDirectionScale(float value)
        {
            _directionArrow.localRotation = Quaternion.Euler(0,0,value);
        }

        private void OnDisable()
        {
            DirectionScaleChanged -= OnDirectionScaleChanged;
            PowerScaleChanged -= OnPowerScaleChanged;
            ThrowStarted -= OnThrowStarted;
        }
    }
}