using System;
using Services;
using UnityEngine;

namespace Network
{
    public class NetworkPlayerHandler : BaseGameHandler<NetworkPlayerHandler>
    {
        [SerializeField] private string _playerId;
        [SerializeField] private string _username;

        private void Start()
        {
            if (_playerId == string.Empty)
            {
                _playerId = Guid.NewGuid().ToString();
            }
        }

        public string GetPlayerId() => _playerId;
        public void SetPlayerId(string id) => _playerId = id; 
        public string GetUsername() => _username;
        public void SetUsername(string username) => _username = username;
    }
}