using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class ParallaxLayers : MonoBehaviour
    {
        [SerializeField] private List<Transform> _layers = new List<Transform>();
        [SerializeField] private List<Vector2> _effectMultiplicators = new List<Vector2>();

        private Transform _cameraTransform;
        private Vector3 _previousCameraPos;

        public List<Transform> Layers => _layers;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
            _previousCameraPos = _cameraTransform.position;
        }

        private void FixedUpdate()
        {
            if (_cameraTransform.position - _previousCameraPos == Vector3.zero)
                return;

            for(int i = 0; i < _layers.Count; i++)
            {
                Vector3 delta = _cameraTransform.position - _previousCameraPos;
                _layers[i].position += new Vector3(delta.x * _effectMultiplicators[i].x, delta.y * _effectMultiplicators[i].y, 0);
            }

            _previousCameraPos = _cameraTransform.position;
        }
    }
}