using System;
using UnityEngine;
using Player;

namespace Level
{
    [RequireComponent(typeof(Collider2D))]
    public class ArtifactPiece : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Collider2D _collider;
        public static event Action ArtifactPieceCollected;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponentInParent<Viking>())
            {
                ArtifactPieceCollected?.Invoke();
                _renderer.enabled = false;
                _collider.enabled = false;
            }
        }
    }
}
