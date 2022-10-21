using System;
using Player.Throws.ThrowDatas;
using UnityEngine;

namespace Player.Throws
{
    public class SimpleThrow : BaseThrow
    {
        [SerializeField] private ScriptableThrowData _throwData;

        private Coroutine _throwCoroutine;

        private Vector2[] _points;

        public void DoSimpleThrow(PlayerTransformController controller, float directionAngle, float power)
        {
            CalculatedThrowData calculatedThrowData = CalculatedThrowDataPoints(directionAngle, power,
                controller.GetPosition(), _throwData.GetThrowData().Weight);
            _throwCoroutine = StartCoroutine(ThrowCoroutinePoints(controller, calculatedThrowData));
        }

        public void StopThrow()
        {
            StopCoroutine(_throwCoroutine);
        }

        // private CalculatedThrowData CalculateThrowData(float directionAngle, float power)
        // {
        //     ThrowData data = _throwData.GetThrowData();
        //     float radAngle = Mathf.Abs(directionAngle + 90) * Mathf.Deg2Rad;
        //
        //     float velocity = power * data.Weight;
        //     float duration = 2 * velocity * Mathf.Sin(radAngle) / 9.81f;
        //     float length = velocity * Mathf.Cos(radAngle) * duration;
        //     float maxHeightTime = velocity * Mathf.Sin(radAngle) / 9.81f;
        //     float height = Mathf.Sin(radAngle) * maxHeightTime * velocity - (9.81f * Mathf.Pow(maxHeightTime, 2) / 2);
        //
        //     Debug.Log($"height = {height}, maxHeightTime = {maxHeightTime}, angle = {directionAngle + 90}, length = {length}, duration = {duration}");
        //     return new CalculatedThrowData(height, length, duration, data.Curve);
        // }

        private CalculatedThrowData CalculatedThrowDataPoints(float directionAngle, float power, Vector2 originPos,
            float weight)
        {
            directionAngle = (directionAngle + 90) % 360;

            float radAngle = Mathf.Abs(directionAngle) * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radAngle);

            float velocity = power * weight;
            float duration = (velocity * sin + Mathf.Sqrt(Mathf.Pow(velocity * sin, 2))) / 9.81f;

            float deltaTime = 1f / 90;
            int pointsCount = (int)((duration) / deltaTime);

            Vector2[] points = new Vector2[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                float t = deltaTime * i;
                float x = originPos.x + velocity * Mathf.Cos(radAngle) * t;
                float y = originPos.y + velocity * Mathf.Sin(radAngle) * t - (9.81f * Mathf.Pow(t, 2) / 2);
                points[i] = new Vector2(x, y);
            }

            _points = points;

            Debug.Log($"duration={duration}, pointsCount={pointsCount}, angle = {directionAngle}");
            return new CalculatedThrowData(points, deltaTime);
        }

        public void DoSimpleDash(PlayerTransformController controller, float power)
        {
            CalculatedThrowData calculatedThrowData =
                CalculateThrowDataDash(power, controller.GetPosition(), _throwData.GetThrowData().Weight);
            _throwCoroutine = StartCoroutine(ThrowCoroutinePoints(controller, calculatedThrowData));
        }

        private CalculatedThrowData CalculateThrowDataDash(float power, Vector2 originPos, float weight)
        {
            float velocity = power * weight;
            float duration = (-velocity + Mathf.Sqrt(Mathf.Pow(velocity, 2) + 2 * 9.81f * originPos.y)) / 9.81f;
            float deltaTime = 1f / 90;
            int pointsCount = (int)((duration) / deltaTime);

            Vector2[] points = new Vector2[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                float t = deltaTime * i;
                float y = originPos.y - velocity * t - (9.81f * Mathf.Pow(t, 2) / 2);
                points[i] = new Vector2(originPos.x, y);
            }

            Debug.Log($"duration={duration}, pointsCount={pointsCount}");
            return new CalculatedThrowData(points, deltaTime);
        }

        private void OnDrawGizmos()
        {
            if (_points is null) return;
            foreach (var point in _points)
            {
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}