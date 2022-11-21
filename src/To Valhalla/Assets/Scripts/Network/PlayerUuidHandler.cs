using System;
using UnityEngine;

namespace Network
{
    public class PlayerUuidHandler : MonoBehaviour
    {
        [SerializeField] private string _playerId;

        private void Start()
        {
            if (_playerId == string.Empty)
            {
                _playerId = Guid.NewGuid().ToString();
            }
        }

        public string GetUuid()
        {
            return _playerId;
        }
    }
}