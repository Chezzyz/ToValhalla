using System;
using System.Collections;
using Input;
using Services;
using UnityEngine;

namespace View
{
    public class ThrowHandsView : MonoBehaviour
    {
        [SerializeField] private Transform _handsPivot;
        [SerializeField] private float _rotationSpeed;
        
        private void OnEnable()
        {
            ThrowScalesController.ThrowStarted += OnThrowStarted;
            StartSessionHandler.SessionStarted += OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            StartCoroutine(HandsRotationCoroutine());
        }

        private IEnumerator HandsRotationCoroutine()
        {
            while (true)
            {
                float currentRot = _handsPivot.eulerAngles.z;
                _handsPivot.localRotation = Quaternion.Euler(0, 0, currentRot + _rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private void OnThrowStarted(float directionAngle, float _)
        {
            _handsPivot.localRotation = Quaternion.Euler(0, 0, 0);
            StopAllCoroutines();
        }

        private void OnDisable()
        {
            ThrowScalesController.ThrowStarted -= OnThrowStarted;
            StartSessionHandler.SessionStarted -= OnSessionStarted;
        }
    }
}