using System;
using System.Collections;
using Player;
using Services;
using UnityEngine;

namespace View
{
    public class BackgroundHandler : MonoBehaviour
    {
        [SerializeField] private PlayerTransformController _playerTransform;
        [SerializeField] private GameObject _bgLeft;
        [SerializeField] private GameObject _bgCenter;
        [SerializeField] private GameObject _bgRight;
        [SerializeField] private float _bgHalfSizeX;

        private void OnEnable()
        {
            StartSessionHandler.SessionStarted += OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            StartCoroutine(MoveBackgroundCoroutine());
        }

        private IEnumerator MoveBackgroundCoroutine()
        {
            while (true)
            {
                if (_playerTransform.GetPosition().x > _bgCenter.transform.position.x + _bgHalfSizeX)
                {
                    _bgLeft.transform.position += new Vector3(_bgHalfSizeX * 6, 0, 0);
                    (_bgRight, _bgLeft, _bgCenter) = (_bgLeft, _bgCenter, _bgRight);
                }
                else if (_playerTransform.GetPosition().x < _bgCenter.transform.position.x - _bgHalfSizeX)
                {
                    _bgRight.transform.position -= new Vector3(_bgHalfSizeX * 6, 0, 0);
                    (_bgCenter, _bgRight, _bgLeft) = (_bgLeft, _bgCenter, _bgRight);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void OnDisable()
        {
            StartSessionHandler.SessionStarted -= OnSessionStarted;
        }
    }
}
