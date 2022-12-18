using System;
using Input;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider2D))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Transform _anchorPoint;

        public static event Action ObstacleTouched;
        
        public static float AdditionalVelocity { get; set; }

        private void OnTriggerEnter2D(Collider2D col)
        {
            ThrowController controller = col.GetComponentInParent<ThrowController>();
            if (controller is not null)
            {
                if(col.transform.position.y <= _anchorPoint.position.y) return;
                Rebound(controller, col);
            }
        }

        private void Rebound(ThrowController throwController, Collider2D col)
        {
            float angle = Vector2.Angle(col.transform.position - _anchorPoint.transform.position, Vector2.right);
            ObstacleTouched?.Invoke();
            throwController.StopThrow();
            throwController.Throw(angle, throwController.GetCurrentVelocity().velocity + AdditionalVelocity);
        }
    }
}