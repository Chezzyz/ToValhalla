using Hammers;
using UnityEngine;

namespace Player.Throws
{
    public class SimpleThrow : BaseThrow
    {
        private Coroutine _throwCoroutine;

        public void DoSimpleThrow(PlayerTransformController controller, ScriptableHammerData hammerData, float directionAngle, float velocity)
        {
            CalculatedThrowData calculatedThrowData = CalculatedThrowDataPoints(directionAngle, velocity,
                controller.GetPosition(), hammerData);
            _throwCoroutine = StartCoroutine(ThrowCoroutinePoints(controller, calculatedThrowData));
        }

        public void StopThrow()
        {
            StopCoroutine(_throwCoroutine);
            SetIsInThrow(false);
        }

        private CalculatedThrowData CalculatedThrowDataPoints(float directionAngle, float velocity, Vector2 originPos,
            ScriptableHammerData hammerData)
        {
            float radAngle = Mathf.Abs(directionAngle) * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radAngle);
            
            float duration = (velocity * sin + Mathf.Sqrt(Mathf.Pow(velocity * sin, 2) + 2 * 9.81f * originPos.y)) / 9.81f;

            float deltaTime = 1f / 40;
            int pointsCount = (int)((duration) / deltaTime);

            Vector2[] points = new Vector2[pointsCount];
            for (int i = 1; i <= pointsCount; i++)
            {
                float t = deltaTime * i;
                float x = originPos.x + velocity * Mathf.Cos(radAngle) * t;
                float y = originPos.y + velocity * Mathf.Sin(radAngle) * t - (9.81f * Mathf.Pow(t, 2) / 2);
                points[i-1] = new Vector2(x, y);
            }

            Debug.Log($"duration={duration}, pointsCount={pointsCount}, angle = {directionAngle}");
            return new CalculatedThrowData(points, deltaTime, velocity, radAngle, hammerData);
        }

        public void DoSimpleDash(PlayerTransformController controller, ScriptableHammerData hammerData, float power)
        {
            CalculatedThrowData calculatedThrowData =
                CalculateThrowDataDash(power, controller.GetPosition(), hammerData);
            _throwCoroutine = StartCoroutine(ThrowCoroutinePoints(controller, calculatedThrowData));
        }

        private CalculatedThrowData CalculateThrowDataDash(float velocity, Vector2 originPos, ScriptableHammerData hammerData)
        {
            float duration = (-velocity + Mathf.Sqrt(Mathf.Pow(velocity, 2) + 2 * 9.81f * originPos.y)) / 9.81f;
            float deltaTime = 1f / 40;
            int pointsCount = (int)((duration) / deltaTime);

            Vector2[] points = new Vector2[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                float t = deltaTime * i;
                float y = originPos.y - velocity * t - (9.81f * Mathf.Pow(t, 2) / 2);
                points[i] = new Vector2(originPos.x, y);
            }

            Debug.Log($"duration={duration}, pointsCount={pointsCount}");
            return new CalculatedThrowData(points, deltaTime, velocity, 270 * Mathf.Deg2Rad, hammerData);
        }
    }
}