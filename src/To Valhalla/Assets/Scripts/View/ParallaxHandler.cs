using System;
using System.Collections;
using Player;
using Services;
using UnityEngine;

namespace View
{
    public class ParallaxHandler : MonoBehaviour
    {
        [SerializeField] private PlayerTransformController _playerTransform;
        [SerializeField] private ParallaxLayers _parallaxLeft;
        [SerializeField] private ParallaxLayers _parallaxCenter;
        [SerializeField] private ParallaxLayers _parallaxRight;
        [SerializeField] private float _parallaxHalfSizeX;

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
                for (int i = 0; i < _parallaxCenter.Layers.Count; i++)
                {
                    if (_playerTransform.GetPosition().x >
                        _parallaxCenter.Layers[i].transform.position.x + _parallaxHalfSizeX)
                    {
                        _parallaxLeft.Layers[i].transform.position += new Vector3(_parallaxHalfSizeX * 6, 0, 0);

                        (_parallaxRight.Layers[i], _parallaxLeft.Layers[i], _parallaxCenter.Layers[i])
                            = (_parallaxLeft.Layers[i], _parallaxCenter.Layers[i], _parallaxRight.Layers[i]);
                    }
                    else if (_playerTransform.GetPosition().x <
                             _parallaxCenter.Layers[i].transform.position.x - _parallaxHalfSizeX)
                    {
                        _parallaxRight.Layers[i].transform.position -= new Vector3(_parallaxHalfSizeX * 6, 0, 0);

                        (_parallaxCenter.Layers[i], _parallaxRight.Layers[i], _parallaxLeft.Layers[i])
                            = (_parallaxLeft.Layers[i], _parallaxCenter.Layers[i], _parallaxRight.Layers[i]);
                    }
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