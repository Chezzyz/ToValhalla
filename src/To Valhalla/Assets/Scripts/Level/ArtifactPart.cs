using System;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider2D))]
    public class ArtifactPart : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Collider2D _collider;
        public static event Action ArtifactPartCollected;

        private void OnTriggerEnter2D(Collider2D col)
        {
            Player.Player player = col.GetComponentInParent<Player.Player>();

            if (player)
            {
                ArtifactPartCollected?.Invoke();
                player.AddArtifactPart(1);
                _renderer.enabled = false;
                _collider.enabled = false;
            }
        }
    }
}
