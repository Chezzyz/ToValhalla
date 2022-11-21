using UnityEngine;

namespace Player
{
    public class PlayerFlightData
    {
        private float _heightRaiseCoeffRelativeToYCoord = 1f;
        private float _currentMaxHeight = 0.0f;
        private float _maxHeight = 0.0f;

        private float _currentFlightTime;

        public PlayerFlightData(float heightRaiseCoeffRelativeToYCoord)
        {
            /*Это коэффициент, как соотносится высота по Y в юнити с метрами высоты в вымышленном мире
             * Типа если мы пролетели 127 юнити единиц, а коэф у нас 10 например, то нам покажет что
             * мы пролетели в игре 1270 м.
            */
            _heightRaiseCoeffRelativeToYCoord = heightRaiseCoeffRelativeToYCoord;
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
        }

        public int GetCurrentMaxFlightHeight() => Mathf.FloorToInt(_currentMaxHeight * _heightRaiseCoeffRelativeToYCoord);
        public float GetCurrentFlightTime() => _currentFlightTime;

        public void Reset()
        {
            _currentFlightTime = 0.0f;
            _currentFlightTime = 0.0f;
        }
    }
}
