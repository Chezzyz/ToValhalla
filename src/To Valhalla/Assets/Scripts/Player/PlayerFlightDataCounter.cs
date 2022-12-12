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
            /*Это коэффициент, как соотносится высота по Y в юнити с метрами высоты в вымышленном мире
             * Типа если мы пролетели 127 юнити единиц, а коэф у нас 10 например, то нам покажет что
             * мы пролетели в игре 1270 м.
            */
            _heightRaiseCoefRelativeToYCoord = heightRaiseCoefRelativeToYCoord;
        }

        public void Update(float deltaTime, Transform transform)
        {
            _currentFlightTime += deltaTime;

            /*Ещё один спорный момент, стоит ли считать высоту в юнити координатах, или лучше связать это с системой точек
             * которые ты генерируешь в скриптах полёта. То есть сейчас трекинг максимальной высоты викинга зависит чисто
             * от его Transform, а не от твоих скриптов, но если нам эта высота нужна чисто чтобы показать результат и записать в бд,
             * то я считаю такой точностью мы можем принебречь, главное ведь что она для всех игроков справедлива.
            */
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
