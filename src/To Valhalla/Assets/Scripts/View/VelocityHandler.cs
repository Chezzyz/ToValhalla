using System;
using Cinemachine;
using Player.Throws;
using UnityEngine;

namespace View
{
    public class VelocityHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxVerticalSpeed;
        [SerializeField] private float _baseSize;
        [SerializeField] private float _minSpeedCameraScale;
        [SerializeField] private float _maxSpeedCameraScale;
        [SerializeField] private float _positiveSpeedCameraOffset;
        [SerializeField] private float _negativeSpeedCameraOffset;

        private void OnEnable()
        {
            BaseThrow.VelocityChanged += OnVelocityChanged;
        }

        private void OnVelocityChanged(float velocity, float verticalVelocity)
        {
            SetCameraSize(_camera, _baseSize,
                Mathf.Lerp(_minSpeedCameraScale, _maxSpeedCameraScale, Mathf.Clamp01(velocity / _maxSpeed)));
            
            // SetCameraOffsetY(_camera,
            //     Mathf.Lerp(_negativeSpeedCameraOffset, _positiveSpeedCameraOffset,
            //         Mathf.InverseLerp(-_maxVerticalSpeed, _maxVerticalSpeed, verticalVelocity)));
        }

        private void SetCameraSize(CinemachineVirtualCamera virtualCamera, float baseSize, float scale)
        {
            virtualCamera.m_Lens.OrthographicSize = baseSize * scale;
        }

        private void SetCameraOffsetY(CinemachineVirtualCamera virtualCamera, float offset)
        {
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset =
                new Vector3(0, offset, 0);
        }

        private void OnDisable()
        {
            BaseThrow.VelocityChanged -= OnVelocityChanged;
        }
    }
}