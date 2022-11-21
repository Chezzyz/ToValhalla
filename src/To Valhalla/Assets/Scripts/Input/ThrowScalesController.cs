using System;
using System.Collections;
using Services;
using UnityEngine;

namespace Input
{
    public class ThrowScalesController : MonoBehaviour
    {
        [SerializeField] private float _directionScaleSpeed;
        [SerializeField] private float _powerScaleSpeed;

        [SerializeField] private float _greenScaleValue;
        [SerializeField] private float _yellowScaleValue;
        [SerializeField] private float _redScaleValue;

        public static event Action<float, float> ThrowStarted;
        public static event Action<float> DirectionScaleChanged;
        public static event Action<float> PowerScaleChanged;
        public static event Action PowerScaleFailed;

        private float _throwDirectionAngle;
        private Coroutine _directionCoroutine;

        private float _throwPower;
        private Coroutine _powerCoroutine;

        private bool _isPreparationState = true;

        private void OnEnable()
        {
            StartSessionHandler.SessionStarted += OnSessionStarted;
            InputHandler.FingerUp += OnFingerUp;
            DirectionScaleChanged += OnDirectionScaleChanged;
            PowerScaleChanged += OnPowerScaleChanged;
        }

        private void OnPowerScaleChanged(float value)
        {
            _throwPower = value;
        }

        private void OnDirectionScaleChanged(float value)
        {
            _throwDirectionAngle = value;
        }

        private void OnSessionStarted()
        {
            _isPreparationState = true;
            _directionCoroutine = StartCoroutine(DirectionScaleCoroutine(_directionScaleSpeed));
            _powerCoroutine = StartCoroutine(PowerScaleCoroutine(_powerScaleSpeed));
        }

        private void OnFingerUp(float _)
        {
            if (!_isPreparationState) return;
            StopCoroutine(_directionCoroutine);
            StopCoroutine(_powerCoroutine);
            ThrowStarted?.Invoke((_throwDirectionAngle + 90) % 360, _throwPower);
            _isPreparationState = false;
        }

        private IEnumerator DirectionScaleCoroutine(float speed)
        {
            float scaleValue = 0;
            while (true)
            {
                while (scaleValue <= 360)
                {
                    scaleValue += speed * Time.deltaTime;
                    DirectionScaleChanged?.Invoke(scaleValue);
                    yield return null;
                }

                scaleValue = 0;
            }
        }

        private IEnumerator PowerScaleCoroutine(float speed)
        {
            float scaleValue = 0;
            while (true)
            {
                while (scaleValue <= 100)
                {
                    scaleValue += speed * Time.deltaTime;
                    PowerScaleChanged?.Invoke(scaleValue);
                    yield return null;
                }
                
                PowerScaleFailed?.Invoke();
                break;
            }
        }

        private void OnDisable()
        {
            StartSessionHandler.SessionStarted -= OnSessionStarted;
            InputHandler.FingerUp -= OnFingerUp;
            DirectionScaleChanged -= OnDirectionScaleChanged;
            PowerScaleChanged -= OnPowerScaleChanged;
        }
    }
}