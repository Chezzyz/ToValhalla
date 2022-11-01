using Input;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider2D))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Transform _anchorPoint;

        private void OnTriggerEnter2D(Collider2D col)
        {
            ThrowController controller = col.GetComponentInParent<ThrowController>();
            if (controller is not null)
            {
                float angle = Vector2.Angle(col.transform.position - _anchorPoint.transform.position, Vector2.right);
                if(col.transform.position.y <= _anchorPoint.position.y) return;
                controller.StopThrow();
                controller.Throw(angle, controller.GetCurrentVelocity().velocity);
            }
        }
    }
}