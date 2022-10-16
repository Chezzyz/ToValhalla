using System;
using System.Collections;
using System.Linq;
using Player.Throws.ThrowDatas;
using UnityEngine;

namespace Player.Throws
{
    public abstract class BaseThrow : MonoBehaviour
    {
        private bool _isInThrow;

        protected Coroutine _currentThrow;

        protected virtual IEnumerator ThrowCoroutine(
            PlayerTransformController playerTransformController,
            CalculatedThrowData throwData)
        {
            float expiredTime = 0.0f;

            Vector2 originPosition = playerTransformController.GetPosition();
            float maxProgress = 1;
            
            float progress = 0;

            while (progress < maxProgress)
            {
                expiredTime += Time.deltaTime;

                progress = expiredTime / throwData.Duration;

                float nextY = throwData.Height * throwData.Curve.Evaluate(progress);
                float nextX = throwData.Length * progress;
                
                Vector2 nextPosition = originPosition + new Vector2(nextX, nextY);

                Vector2 normalized = (nextPosition - playerTransformController.GetPosition()).normalized;
                float angle = Mathf.Acos(normalized.x) * Mathf.Sign(normalized.y) * Mathf.Rad2Deg;
                playerTransformController.SetViewRotationEuler(new Vector3(0,0, angle - 90));

                playerTransformController.SetPosition(nextPosition);

                yield return null;
            }
        }
    }
}