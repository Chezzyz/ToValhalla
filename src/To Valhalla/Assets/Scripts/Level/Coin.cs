using System;
using UnityEngine;
using Player;

namespace Level
{
    [RequireComponent(typeof(Collider2D))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Collider2D _collider;
        public static event Action CoinCollected;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponentInParent<Viking>())
            {
                CoinCollected?.Invoke();
                _renderer.enabled = false;
                _collider.enabled = false;
            }
        }
    }
}
