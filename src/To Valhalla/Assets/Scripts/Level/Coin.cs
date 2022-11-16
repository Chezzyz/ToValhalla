using System;
using UnityEngine;

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
            Player.Player player = col.GetComponentInParent<Player.Player>();
            if (player is not null)
            {
                CoinCollected?.Invoke();
                player.AddCoins(1);
                _renderer.enabled = false;
                _collider.enabled = false;
            }
        }
    }
}
