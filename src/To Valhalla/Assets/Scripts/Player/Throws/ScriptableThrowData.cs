using System;
using Hammers;
using UnityEngine;

namespace Player.Throws
{
    public struct CalculatedThrowData
    {
        public readonly Vector2[] Points;
        public readonly float DeltaTime;
        public ScriptableHammerData HammerData;
        private readonly float _velocity;
        private readonly float _radAngle;

        public float GetCurrentVelocity(float t) => Mathf.Sqrt(Mathf.Pow(_velocity * Mathf.Cos(_radAngle), 2) +
                                                               Mathf.Pow(GetCurrentVerticalVelocity(t), 2));

        public float GetCurrentVerticalVelocity(float t) => _velocity * Mathf.Sin(_radAngle) - 9.81f * t;

        public CalculatedThrowData(Vector2[] points, float deltaTime, float velocity, float radAngle, ScriptableHammerData hammerData)
        {
            Points = points;
            DeltaTime = deltaTime;
            _velocity = velocity;
            _radAngle = radAngle;
            HammerData = hammerData;
        }
    }
}