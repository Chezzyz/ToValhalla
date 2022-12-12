using System;
using Player;
using TMPro;
using UnityEngine;

namespace View
{
    public class HeightTextHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            PlayerFlightDataCounter.CurrentHeightChanged += OnCurrentHeightChanged;
        }

        private void OnCurrentHeightChanged(int height)
        {
            _text.text = $"{height}m";
        }

        private void OnDisable()
        {
            PlayerFlightDataCounter.CurrentHeightChanged += OnCurrentHeightChanged;
        }
    }
}