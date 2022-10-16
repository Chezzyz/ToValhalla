using System;
using System.Collections;
using Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Input
{
    public class ThrowScalesController : MonoBehaviour
    {
        [SerializeField] private float _directionScaleSpeed;
        [SerializeField] private float _directionScaleLimit;

        [SerializeField] private float _powerScaleLimit;
        [SerializeField] private float _powerScaleSpeed;

        public static event Action<float, float> ThrowStarted;
        public static event Action<ScaleType, float> ScaleChanged;

        private float _throwDirectionAngle;
        private Coroutine _directionCoroutine;

        private float _throwPower;
        private Coroutine _powerCoroutine;

        private bool _isPreparationState = true;

        public enum ScaleType
        {
            Power,
            Direction
        }

        private void OnEnable()
        {
            StartSessionHandler.SessionStarted += OnSessionStarted;
            InputHandler.FingerDown += OnFingerDown;
            InputHandler.FingerUp += OnFingerUp;
            ScaleChanged += OnScaleChanged;
        }

        private void OnScaleChanged(ScaleType scaleType, float value)
        {
            if (scaleType == ScaleType.Power) _throwPower = value;
            if (scaleType == ScaleType.Direction) _throwDirectionAngle = value;
        }

        private void OnSessionStarted()
        {
            _isPreparationState = true;
            _directionCoroutine =
                StartCoroutine(ScaleCoroutine(_throwDirectionAngle, -_directionScaleLimit, _directionScaleLimit,
                    _directionScaleSpeed,
                    ScaleType.Direction));
        }

        private void OnFingerUp(float age)
        {
            if (!_isPreparationState) return;
            StopCoroutine(_powerCoroutine);
            ThrowStarted?.Invoke(_throwDirectionAngle, _throwPower);
            _isPreparationState = false;
        }

        private void OnFingerDown()
        {
            if (!_isPreparationState) return;
            StopCoroutine(_directionCoroutine);
            _powerCoroutine =
                StartCoroutine(ScaleCoroutine(_throwPower, 0, _powerScaleLimit, _powerScaleSpeed, ScaleType.Power));
        }

        private IEnumerator ScaleCoroutine(float scaleValue, float botLimit, float topLimit, float speed,
            ScaleType scaleType)
        {
            while (true)
            {
                while (scaleValue > botLimit)
                {
                    scaleValue -= speed * Time.deltaTime;
                    ScaleChanged?.Invoke(scaleType, scaleValue);
                    yield return null;
                }

                while (scaleValue < topLimit)
                {
                    scaleValue += speed * Time.deltaTime;
                    ScaleChanged?.Invoke(scaleType, scaleValue);
                    yield return null;
                }
            }
        }

        private void OnDisable()
        {
            StartSessionHandler.SessionStarted -= OnSessionStarted;
            InputHandler.FingerDown -= OnFingerDown;
            InputHandler.FingerUp -= OnFingerUp;
        }
    }
}