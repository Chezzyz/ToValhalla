using System;
using System.Collections.Generic;
using System.Linq;
using Hammers;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CirclePowerScaleView : MonoBehaviour
    {
        [SerializeField] private List<Image> _sectors = new ();
        [SerializeField] private Transform _scaleLinePivot;
        [SerializeField] private Image _scaleLine;

        private void OnEnable()
        {
            StartSessionHandler.SessionStarted += OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            _scaleLine.enabled = true;
            ScriptableHammerData hammerData = FindObjectOfType<HammerHandler>().GetCurrentHummerData();
            SetScaleSectors(Image.Origin360.Top, 0f, hammerData.GetScalePartsInPercent().ToList());
        }

        public void SetScaleSectors(Image.Origin360 origin, float startOffsetInPercent, List<float> valuesInPercents)
        {
            if (valuesInPercents.Count != _sectors.Count)
                throw new ArgumentException("Count of values doesn't match sectors count");

            float previousSectorAngleOffset = -(360f * startOffsetInPercent / 100f);
            SetScaleLinePivot(origin, previousSectorAngleOffset);

            for (int i = 0; i < _sectors.Count; i++)
            {
                _sectors[_sectors.Count - 1 - i].fillOrigin = (int)origin;
                _sectors[_sectors.Count - 1 - i].fillAmount = valuesInPercents[i] / 100f;
                _sectors[_sectors.Count - 1 - i].transform.localEulerAngles = new Vector3(0f, 0f, previousSectorAngleOffset);
                previousSectorAngleOffset -= (360 * valuesInPercents[i] / 100f);
            }
        }

        private void SetScaleLinePivot(Image.Origin360 origin, float offset)
        {
            switch (origin)
            {
                case Image.Origin360.Left:
                    offset += 90f;
                    break;
                case Image.Origin360.Bottom:
                    offset += 180f;
                    break;
                case Image.Origin360.Right:
                    offset += 270f;
                    break;
                default:
                    break;
            }

            _scaleLinePivot.localEulerAngles = new Vector3(0f, 0f, offset);
        }

        private void OnDisable()
        {
            StartSessionHandler.SessionStarted -= OnSessionStarted;
        }
        
    }
}