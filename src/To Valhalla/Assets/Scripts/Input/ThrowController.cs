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
        [SerializeField] private float _dashPower;

        private bool _isInDash;
        
        private void OnEnable()
        {
            ThrowScalesController.ThrowStarted += OnThrowStarted;
            InputHandler.FingerDown += OnFingerDown;
        }
       
        private void OnThrowStarted(float directionAngle, float powerPercent)
        {
            _simpleThrow.DoSimpleThrow(_playerTransformController, directionAngle, (powerPercent / 100) * _maxPower);
        }
        
        private void OnFingerDown()
        {
            if (_simpleThrow.IsInThrow() && !_isInDash)
            {
                _simpleThrow.StopThrow();
                _simpleThrow.DoSimpleDash(_playerTransformController, _dashPower);
                _isInDash = true;
            }
        }
        
        private void OnDisable()
        {
            ThrowScalesController.ThrowStarted -= OnThrowStarted;
            InputHandler.FingerDown -= OnFingerDown;
        }
    }
}