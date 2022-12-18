using System;
using UnityEngine;

namespace Player
{
    public class PlayerFlightDataCounter
    {
        private float _heightRaiseCoefRelativeToYCoord = 1f;
        private float _currentMaxHeight;
        private int _currentHeight;

        private float _currentFlightTime;

        public static event Action<int> CurrentHeightChanged; 

        public PlayerFlightDataCounter(float heightRaiseCoefRelativeToYCoord)
        {
            _heightRaiseCoefRelativeToYCoord = heightRaiseCoefRelativeToYCoord;
        }

        public void Update(float deltaTime, Transform transform)
        {
            _currentFlightTime += deltaTime;
            
            if (transform.position.y > _currentMaxHeight)
            {
                _currentMaxHeight = transform.position.y;
            }

            if ((int)transform.position.y != _currentHeight)
            {
                _currentHeight = (int)transform.position.y;
                CurrentHeightChanged?.Invoke(_currentHeight);
            }
        }

        public int GetCurrentMaxFlightHeight() => Mathf.FloorToInt(_currentMaxHeight * _heightRaiseCoefRelativeToYCoord);
        public float GetCurrentFlightTime() => _currentFlightTime;

        public void Reset()
        {
            _currentFlightTime = 0.0f;
            _currentFlightTime = 0.0f;
        }
    }
}
