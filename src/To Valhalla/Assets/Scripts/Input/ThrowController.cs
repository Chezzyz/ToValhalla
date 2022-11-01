using System;
using System.Collections;
using Player;
using Player.Throws;
using Services;
using Unity.VisualScripting;
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
        private (float velocity, float verticalVelocity) _currentVelocity;
        
        private void OnEnable()
        {
            ThrowScalesController.ThrowStarted += OnThrowStarted;
            InputHandler.FingerDown += OnFingerDown;
            BaseThrow.VelocityChanged += OnVelocityChanged;
        }

        public (float velocity, float verticalVelocity) GetCurrentVelocity()
        {
            return _currentVelocity;
        }

        public void Throw(float directionAngle, float power)
        {
            _simpleThrow.DoSimpleThrow(_playerTransformController, directionAngle, power);
            _isInDash = false;
        }

        public void StopThrow()
        {
            _simpleThrow.StopThrow();
        }
       
        private void OnThrowStarted(float directionAngle, float powerPercent)
        {
            Throw(directionAngle, (powerPercent / 100) * _maxPower);
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
        
        private void OnVelocityChanged(float velocity, float verticalVelocity)
        {
            _currentVelocity.velocity = velocity;
            _currentVelocity.verticalVelocity = verticalVelocity;
        }
        
        private void OnDisable()
        {
            ThrowScalesController.ThrowStarted -= OnThrowStarted;
            InputHandler.FingerDown -= OnFingerDown;
            BaseThrow.VelocityChanged -= OnVelocityChanged;
        }
    }
}