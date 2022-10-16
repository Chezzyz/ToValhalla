﻿using System;
using UnityEngine;
using static Input.ThrowScalesController;

namespace View
{
    public class ThrowScalesView : MonoBehaviour
    {
        [SerializeField] private Transform _directionArrow;
        [SerializeField] private RectTransform _powerScale;
        [SerializeField] private float _powerScaleTopLimit;
        [SerializeField] private float _powerScaleBotLimit;
        [SerializeField] private Canvas _scalesParent;

        private void OnEnable()
        {
            ScaleChanged += OnScaleChanged;
            ThrowStarted += OnThrowStarted;
        }

        private void OnThrowStarted(float arg1, float arg2)
        {
            _scalesParent.enabled = false;
        }

        private void OnScaleChanged(ScaleType scaleType, float value)
        {
            if (scaleType == ScaleType.Power)
            {
                ChangePowerScale(value);
            }

            if (scaleType == ScaleType.Direction)
            {
                ChangeDirectionScale(value);
            }
        }

        private void ChangePowerScale(float value)
        {
            float newPosY = Mathf.Lerp(_powerScaleBotLimit, _powerScaleTopLimit, value / 100);
            _powerScale.anchoredPosition = new Vector2(_powerScale.localPosition.x, newPosY);
        }

        private void ChangeDirectionScale(float value)
        {
            _directionArrow.localRotation = Quaternion.Euler(0,0,value);
        }

        private void OnDisable()
        {
            ScaleChanged -= OnScaleChanged;
            ThrowStarted -= OnThrowStarted;
        }
    }
}