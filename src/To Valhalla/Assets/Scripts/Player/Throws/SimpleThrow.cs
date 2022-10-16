using System;
using Player.Throws.ThrowDatas;
using UnityEngine;

namespace Player.Throws
{
    public class SimpleThrow : BaseThrow
    {
        [SerializeField] private ScriptableThrowData _throwData;

        public void DoSimpleThrow(PlayerTransformController controller, float directionAngle, float power)
        {
            CalculatedThrowData calculatedThrowData = CalculateThrowData(directionAngle, power);
            StartCoroutine(ThrowCoroutine(controller, calculatedThrowData));
        }

        private CalculatedThrowData CalculateThrowData(float directionAngle, float power)
        {
            ThrowData data = _throwData.GetThrowData();
            float radAngle = Mathf.Abs(directionAngle + 90) * Mathf.Deg2Rad;

            float velocity = power * data.Weight;
            float duration = 2 * velocity * Mathf.Sin(radAngle) / 9.81f;
            float length = velocity * Mathf.Cos(radAngle) * duration;
            float maxHeightTime = velocity * Mathf.Sin(radAngle) / 9.81f;
            float height = Mathf.Sin(radAngle) * maxHeightTime * velocity - (9.81f * Mathf.Pow(maxHeightTime, 2) / 2);

            Debug.Log($"height = {height}, angle = {directionAngle + 90}, length = {length}, duration = {duration}");
            return new CalculatedThrowData(height, length, duration, data.Curve);
        }
    }
}