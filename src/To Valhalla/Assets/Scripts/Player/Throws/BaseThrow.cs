using System;
using System.Collections;
using UnityEngine;

namespace Player.Throws
{
    public abstract class BaseThrow : MonoBehaviour
    {
        public static event Action<float, float> VelocityChanged;
        public static event Action ThrowCompleted;
        private bool _isInThrow;

        public bool IsInThrow()
        {
            return _isInThrow;
        }

        protected void SetIsInThrow(bool state)
        {
            _isInThrow = state;
        }
        
        protected IEnumerator ThrowCoroutinePoints(PlayerTransformController playerTransformController,
            CalculatedThrowData throwData)
        {
            int index = 0;
            
            _isInThrow = true;
            while (index < throwData.Points.Length)
            {
                Vector2 nextPosition = throwData.Points[index];
                
                RotatePlayer(playerTransformController, nextPosition);

                VelocityChanged?.Invoke(throwData.GetCurrentVelocity(index * throwData.DeltaTime),
                    throwData.GetCurrentVerticalVelocity(index * throwData.DeltaTime));
                
                playerTransformController.SetPosition(nextPosition);
                index++;
                
                yield return new WaitForSeconds(throwData.DeltaTime);
            }
            ThrowCompleted?.Invoke();
            _isInThrow = false;
        }

        private void RotatePlayer(PlayerTransformController playerTransformController, Vector2 nextPosition)
        {
            Vector2 normalizedPosDelta = (nextPosition - playerTransformController.GetPosition()).normalized;
            float angle = Mathf.Acos(normalizedPosDelta.x) * Mathf.Sign(normalizedPosDelta.y) * Mathf.Rad2Deg;
            playerTransformController.SetViewRotationEuler(new Vector3(0,0, angle - 90));
        }
    }
}