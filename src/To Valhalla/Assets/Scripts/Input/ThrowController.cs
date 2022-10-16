using System;
using System.Collections;
using Player;
using Player.Throws;
using Services;
using UnityEngine;

namespace Input
{
    public class ThrowController : MonoBehaviour
    {
        [SerializeField] private PlayerTransformController _playerTransformController;
        [SerializeField] private SimpleThrow _simpleThrow;
        [SerializeField] private float _maxPower;
        
        private void OnEnable()
        {
            ThrowScalesController.ThrowStarted += OnThrowStarted;
        }

        private void OnThrowStarted(float directionAngle, float powerPercent)
        {
            _simpleThrow.DoSimpleThrow(_playerTransformController, directionAngle, (powerPercent / 100) * _maxPower);
        }
        
        private void OnDisable()
        {
            ThrowScalesController.ThrowStarted -= OnThrowStarted;
        }
    }
}