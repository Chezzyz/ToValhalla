using System;
using System.Collections;
using Lean.Touch;
using Services;
using UnityEngine;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        public static event Action FingerDown;
        public static event Action<float> FingerUp;

        private bool _canSendEvents;
    
        private void OnEnable()
        {
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUp += OnFingerUp;
            StartSessionHandler.SessionStarted += OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            OnSendingEventsStateChanged(true);
        }

        private void OnSendingEventsStateChanged(bool canSendEvents)
        {
            if (canSendEvents)
            {
                StartCoroutine(SetCanSendAfterDelay(true, 0.01f));
            }
            else
            {
                _canSendEvents = false;
            }
        }

        private IEnumerator SetCanSendAfterDelay(bool value, float delay)
        {
            yield return new WaitForSeconds(delay);
            _canSendEvents = value;
        } 
    
        private void OnFingerUp(LeanFinger finger)
        {
            if (_canSendEvents)
            {
                FingerUp?.Invoke(finger.Age);
            }
        }

        private void OnFingerDown(LeanFinger finger)
        {
            if (_canSendEvents)
            {
                FingerDown?.Invoke();
            }
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnFingerUp -= OnFingerUp;
            StartSessionHandler.SessionStarted -= OnSessionStarted;
        }
    }
}
