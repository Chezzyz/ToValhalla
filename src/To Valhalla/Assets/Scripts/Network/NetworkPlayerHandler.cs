using System;
using Services;
using Services.SaveLoad;
using UnityEngine;

namespace Network
{
    public class NetworkPlayerHandler : BaseGameHandler<NetworkPlayerHandler>
    {
        public static event Action<string> PlayerIDSetted;
        public static event Action<string> PlayerNameSetted;

        [SerializeField] private string _playerId;
        [SerializeField] private string _username;

        private void OnEnable()
        {
            SaveLoadSystem.SaveLoaded += OnSaveLoaded;
        }

        private void OnSaveLoaded()
        {
            if (_playerId == string.Empty)
            {
                _playerId = Guid.NewGuid().ToString();
            }
            PlayerIDSetted?.Invoke(_playerId);

            if (_username == "")
            {
                _username = "Player_" + _playerId;
            }
            //else
            //{
            //    PlayerNameSetted?.Invoke(_username);
            //}
        }

        public string GetPlayerId() => _playerId;
        public void SetPlayerId(string id) => _playerId = id; 
        public string GetUsername() => _username;
        public void SetUsername(string username) => _username = username;
    }
}