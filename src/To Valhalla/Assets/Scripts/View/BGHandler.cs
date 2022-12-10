using UnityEngine;
using Player;

public class BGHandler : MonoBehaviour
{
    [SerializeField] private PlayerTransformController _playerTransform;
    [SerializeField] private GameObject _bgLeft;
    [SerializeField] private GameObject _bgCenter;
    [SerializeField] private GameObject _bgRight;
    [SerializeField] private float _bgHalfSizeX;

    private void Update()
    {
        CheckToSwitchBGPositions();
    }

    private void CheckToSwitchBGPositions()
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
    }
}
