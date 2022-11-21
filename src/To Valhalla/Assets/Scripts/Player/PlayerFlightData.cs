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
            /*��� �����������, ��� ����������� ������ �� Y � ����� � ������� ������ � ����������� ����
             * ���� ���� �� ��������� 127 ����� ������, � ���� � ��� 10 ��������, �� ��� ������� ���
             * �� ��������� � ���� 1270 �.
            */
            _heightRaiseCoeffRelativeToYCoord = heightRaiseCoeffRelativeToYCoord;
        }

        public void Update(float deltaTime, Transform transform)
        {
            _currentFlightTime += deltaTime;

            /*��� ���� ������� ������, ����� �� ������� ������ � ����� �����������, ��� ����� ������� ��� � �������� �����
             * ������� �� ����������� � �������� �����. �� ���� ������ ������� ������������ ������ ������� ������� �����
             * �� ��� Transform, � �� �� ����� ��������, �� ���� ��� ��� ������ ����� ����� ����� �������� ��������� � �������� � ��,
             * �� � ������ ����� ��������� �� ����� ����������, ������� ���� ��� ��� ��� ���� ������� �����������.
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
