using System;
using UnityEngine;

namespace Services
{
    public class GameTimeHandler : BaseGameHandler<GameTimeHandler>
    {
        private double _gameTime;

        private void Update()
        {
            _gameTime += Time.deltaTime;
        }
        
        public TimeSpan GetGameTime() => TimeSpan.FromSeconds(_gameTime);

        public void SetGameTime(double gameTime) => _gameTime = gameTime;
    }
}