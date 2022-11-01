using System;
using UnityEngine;

namespace Player.Throws.ThrowDatas
{
    [CreateAssetMenu(fileName = "ThrowData", menuName = "ScriptableObjects/Throws/ThrowData", order = 0)]
    public class ScriptableThrowData : ScriptableObject
    {
        [SerializeField] protected ThrowData _throwData;

        public ThrowData GetThrowData()
        {
            return _throwData;
        }
    }

    [Serializable]
    public struct CalculatedThrowData
    {
        public Vector2[] Points;
        public float DeltaTime;
        public float Velocity;
        public float RadAngle;

        public float GetCurrentVelocity(float t) => Mathf.Sqrt(Mathf.Pow(Velocity * Mathf.Cos(RadAngle), 2) +
                                                               Mathf.Pow(GetCurrentVerticalVelocity(t), 2));

        public float GetCurrentVerticalVelocity(float t) => Velocity * Mathf.Sin(RadAngle) - 9.81f * t;

        public CalculatedThrowData(Vector2[] points, float deltaTime, float velocity, float radAngle)
        {
            Points = points;
            DeltaTime = deltaTime;
            Velocity = velocity;
            RadAngle = radAngle;
        }
    }

    [Serializable]
    public struct ThrowData
    {
        public float Weight;

        public ThrowData(float weight)
        {
            Weight = weight;
        }
    }
}