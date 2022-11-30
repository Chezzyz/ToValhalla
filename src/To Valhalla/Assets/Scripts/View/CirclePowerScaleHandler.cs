using System;
using System.Collections.Generic;
using System.Linq;
using Hammers;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CirclePowerScaleHandler : MonoBehaviour
    {
        [SerializeField] private Image _scaleLine;
        [SerializeField] private CirclePowerScaleView _scaleView;

        private void OnEnable()
        {
            StartSessionHandler.SessionStarted += OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            if (_scaleLine != null)
                _scaleLine.enabled = true;
            ScriptableHammerData hammerData = FindObjectOfType<HammerHandler>().GetCurrentHummerData();
            _scaleView.SetScaleSectors(Image.Origin360.Top, 0f, hammerData.GetScalePartsInPercent().ToList());
        }

        private void OnDisable()
        {
            StartSessionHandler.SessionStarted -= OnSessionStarted;
        }
    }
}