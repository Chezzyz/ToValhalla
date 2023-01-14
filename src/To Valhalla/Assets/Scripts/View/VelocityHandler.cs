using System;
using System.Collections;
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
        [SerializeField] [Range(0, 0.2f)] private float _cameraOffsetSmoothSpeed;
        [SerializeField] [Range(0, 0.2f)] private float _cameraScaleSmoothSpeed;

        private CinemachineFramingTransposer _cinemachineFramingTransposer;

        private void Awake()
        {
            _cinemachineFramingTransposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        private void OnEnable()
        {
            BaseThrow.VelocityChanged += OnVelocityChanged;
            BaseThrow.ThrowCompleted += OnThrowCompleted;
        }

        private void OnThrowCompleted()
        {
            StartCoroutine(ResetCameraCoroutine(_camera, _cinemachineFramingTransposer, _baseSize));
        }

        private IEnumerator ResetCameraCoroutine(CinemachineVirtualCamera virtualCamera,
            CinemachineFramingTransposer framingTransposer, float baseSize)
        {
            while (virtualCamera.m_Lens.OrthographicSize > baseSize)
            {
                SetCameraSizeWithLerp(virtualCamera, baseSize, _minSpeedCameraScale);
                SetCameraOffsetYWithLerp(framingTransposer, 0);
                yield return null;
            }
        }

        private void OnVelocityChanged(float velocity, float verticalVelocity)
        {
            SetCameraSizeWithLerp(_camera, _baseSize,
                Mathf.Lerp(_minSpeedCameraScale, _maxSpeedCameraScale, Mathf.Clamp01(velocity / _maxSpeed)));

            SetCameraOffsetYWithLerp(_cinemachineFramingTransposer,
                Mathf.Lerp(_negativeSpeedCameraOffset, _positiveSpeedCameraOffset,
                    Mathf.InverseLerp(-_maxVerticalSpeed, _maxVerticalSpeed, verticalVelocity)));
        }

        private void SetCameraSizeWithLerp(CinemachineVirtualCamera virtualCamera, float baseSize, float scale)
        {
            float newSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, baseSize * scale,
                _cameraScaleSmoothSpeed);
            virtualCamera.m_Lens.OrthographicSize = newSize;
        }

        private void SetCameraOffsetYWithLerp(CinemachineFramingTransposer framingTransposer, float offset)
        {
            float newY = Mathf.Lerp(framingTransposer.m_TrackedObjectOffset.y, offset, _cameraOffsetSmoothSpeed);

            framingTransposer.m_TrackedObjectOffset = new Vector3(0, newY, 0);
        }

        private void OnDisable()
        {
            BaseThrow.VelocityChanged -= OnVelocityChanged;
            BaseThrow.ThrowCompleted -= OnThrowCompleted;
        }
    }
}