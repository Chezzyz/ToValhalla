﻿using System;
using System.Collections;
using UnityEngine;
using Player;

namespace Level
{
    [RequireComponent(typeof(Collider2D))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private float _collectRadius;
        [SerializeField] private float _defaultColliderRadius;
        public static event Action CoinCollected;
        
        private static float s_colliderRadiusMultiplier = 1;

        private void OnEnable()
        {
            _collider.radius = _defaultColliderRadius * s_colliderRadiusMultiplier;
        }

        public static void SetColliderRadiusMultiplier(float value)
        {
            s_colliderRadiusMultiplier = value;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            PlayerTransformController player = col.GetComponentInParent<PlayerTransformController>();
            if (player)
            {
                StartCoroutine(FollowViking(player));
            }
        }

        private IEnumerator FollowViking(PlayerTransformController player)
        {
            float currentSpeed = 1f;
        
            while (Vector2.Distance(player.GetPosition(), GetCurrentPosition()) > _collectRadius)
            {
                Vector3 distance = player.GetPosition() - GetCurrentPosition();
                MoveParent(distance * (currentSpeed * Time.deltaTime));
                yield return null;
                currentSpeed *= 1.05f;
            }
            Collect();
        }

        private void MoveParent(Vector3 translate)
        {
            transform.parent.Translate(translate);
        }

        private Vector2 GetCurrentPosition()
        {
            Vector3 position = transform.position;
            return new Vector2(position.x, position.y);
        }

        private void Collect()
        {
            CoinCollected?.Invoke();
            _renderer.enabled = false;
            _collider.enabled = false;
        }
        
        private void OnDisable()
        {
            s_colliderRadiusMultiplier = _defaultColliderRadius;
        }
    }
}
