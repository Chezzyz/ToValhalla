using System;
using System.Collections;
using Hammers;
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
        [SerializeField] private HammerHandler _hammerHandler;
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

        public void Throw(float directionAngle, float velocity)
        {
            _simpleThrow.DoSimpleThrow(_playerTransformController, _hammerHandler.GetCurrentHummerData(),
                directionAngle, velocity);
            _isInDash = false;
        }

        public void StopThrow()
        {
            _simpleThrow.StopThrow();
        }

        private void OnThrowStarted(float directionAngle, float powerPercent)
        {
            ScriptableHammerData hammerData = _hammerHandler.GetCurrentHummerData();
            float velocity = (powerPercent / 100) * _maxPower * hammerData.GetPowerMultiplier(powerPercent) *
                             hammerData.GetWeight();
            Throw(directionAngle, velocity);
        }

        private void OnFingerDown()
        {
            if (_simpleThrow.IsInThrow() && !_isInDash)
            {
                _simpleThrow.StopThrow();
                _simpleThrow.DoSimpleDash(_playerTransformController, _hammerHandler.GetCurrentHummerData(),
                    GetCurrentVelocity().velocity);
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