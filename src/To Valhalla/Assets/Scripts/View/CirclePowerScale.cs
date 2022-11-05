using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirclePowerScale : MonoBehaviour
{
    [SerializeField] private List<Image> _sectors = new List<Image>();
    [SerializeField] private Transform _scaleLinePivot;
    [SerializeField] private List<float> _testValues = new List<float>();

    private void Start()
    {
        //For test
        SetScaleSectors(Image.Origin360.Top, 0f, _testValues);
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

            if (i > 0)
            {
                _sectors[_sectors.Count - 1 - i].transform.localEulerAngles = new Vector3(0f, 0f, previousSectorAngleOffset - (360 * valuesInPercents[i] / 100f));
                previousSectorAngleOffset = previousSectorAngleOffset - (360 * valuesInPercents[i] / 100f);
            }
            else
            { 
                _sectors[_sectors.Count - 1 - i].transform.localEulerAngles = new Vector3(0f, 0f, previousSectorAngleOffset);
            }

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
}