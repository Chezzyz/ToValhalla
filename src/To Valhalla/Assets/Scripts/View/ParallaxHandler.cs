using Player;
using UnityEngine;

public class ParallaxHandler : MonoBehaviour
{
    [SerializeField] private PlayerTransformController _playerTransform;
    [SerializeField] private ParallaxLayers _parallaxLeft;
    [SerializeField] private ParallaxLayers _parallaxCenter;
    [SerializeField] private ParallaxLayers _parallaxRight;
    [SerializeField] private float _parallaxHalfSizeX;
    [SerializeField] private int _layersCountToCheck = 0;

    private void Update()
    {
        CheckToSwitchBGPositions();
    }

    private void CheckToSwitchBGPositions()
    {
        int layersCountToCheck = _layersCountToCheck == 0 ? _parallaxCenter.Layers.Count : _layersCountToCheck;

        for (int i = 0; i < layersCountToCheck; i++)
        {
            if (_playerTransform.GetPosition().x > _parallaxCenter.Layers[i].transform.position.x + _parallaxHalfSizeX)
            {
                _parallaxLeft.Layers[i].transform.position += new Vector3(_parallaxHalfSizeX * 6, 0, 0);

                (_parallaxRight.Layers[i], _parallaxLeft.Layers[i], _parallaxCenter.Layers[i]) 
                    = (_parallaxLeft.Layers[i], _parallaxCenter.Layers[i], _parallaxRight.Layers[i]);
            }
            else if (_playerTransform.GetPosition().x < _parallaxCenter.Layers[i].transform.position.x - _parallaxHalfSizeX)
            {
                _parallaxRight.Layers[i].transform.position -= new Vector3(_parallaxHalfSizeX * 6, 0, 0);

                (_parallaxCenter.Layers[i], _parallaxRight.Layers[i], _parallaxLeft.Layers[i]) 
                    = (_parallaxLeft.Layers[i], _parallaxCenter.Layers[i], _parallaxRight.Layers[i]);
            }
        }
    }
}
