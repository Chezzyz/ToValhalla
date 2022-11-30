using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerFlightData _playerFlightData;

        private int _coins = 0;
        private int _artifactParts = 0;

        //Правильнее наверное где-то вызывать Init() у этого Player, в какой-то точке сбора игры, чем тут писать Start()
        private void Start()
        {
            _playerFlightData = new PlayerFlightData(1f);
        }

        private void OnEnable()
        {
            Input.ThrowScalesController.ThrowStarted += OnThrowStarted;
            Throws.BaseThrow.ThrowCompleted += OnThrowCompleted;
        }


        private void OnDisable()
        {
            Input.ThrowScalesController.ThrowStarted -= OnThrowStarted;
            Throws.BaseThrow.ThrowCompleted -= OnThrowCompleted;
        }

        private void OnThrowCompleted()
        {
            StopAllCoroutines();
        }

        private void OnThrowStarted(float arg1, float arg2)
        {
            _playerFlightData.Reset();
            StartCoroutine(FlightDataSimulation(_playerFlightData));
        }

        private IEnumerator FlightDataSimulation(PlayerFlightData playerFlightData)
        {
            while (true)
            {
                yield return null;
                playerFlightData.Update(Time.deltaTime, transform);
            }
        }

        public void AddArtifactPart(int value)
        {
            _artifactParts += value;
        }

        public void AddCoins(int value)
        {
            _coins += value;
        }

        public int GetCurrentCoins() => _coins;
        public int GetCurrentArtifactParts() => _artifactParts;
        public int GetCurrentMaxFlightHeight() => _playerFlightData.GetCurrentMaxFlightHeight();
        public float GetCurrentFlightTime() => _playerFlightData.GetCurrentFlightTime();
    }
}