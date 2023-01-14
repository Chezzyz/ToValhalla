using System;
using Services;
using UnityEngine;

namespace Network
{
    public class NetworkPlayerHandler : BaseGameHandler<NetworkPlayerHandler>
    {
        public static event Action<string> PlayerIDSetted;

        [SerializeField] private string _playerId;
        [SerializeField] private string _username;

        private void Start()
        {
            if (_playerId == string.Empty)
            {
                _playerId = Guid.NewGuid().ToString();
                PlayerIDSetted?.Invoke(_playerId);
            }

            if (_username == "")
            {
                _username = "Player_" + _playerId;
            }
        }

        public string GetPlayerId() => _playerId;
        public void SetPlayerId(string id) => _playerId = id; 
        public string GetUsername() => _username;
        public void SetUsername(string username) => _username = username;
    }
}