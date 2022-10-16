using UnityEngine;

namespace Player
{
    public class PlayerTransformController : MonoBehaviour
    {
        [SerializeField] private Transform _rotationPivot;
        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void SetPosition(Vector2 newPos)
        {
            transform.position = newPos;
        }

        public void SetViewRotationEuler(Vector3 newRot)
        {
            _rotationPivot.transform.rotation = Quaternion.Euler(newRot);
        }
    }
}