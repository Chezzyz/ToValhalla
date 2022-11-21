using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Player.Throws;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public class SessionDataUploader : MonoBehaviour
    {
        [SerializeField] private string _uri;
        [SerializeField] private PlayerUuidHandler _uuidHandler;

        private void OnEnable()
        {
            BaseThrow.ThrowCompleted += OnThrowCompleted;
        }

        private void OnThrowCompleted()
        {
            StartCoroutine(SendData());
        }

        private IEnumerator SendData()
        {
            SessionData data =  new SessionData(_uuidHandler.GetUuid(), "username", DateTime.Now,
                    new TimeSpan(0, 0, 30, 0), TimeSpan.FromSeconds(60),
                    100, "Midgard", 10, 100, 1, 1, 1,
                    new List<SessionData.Score>()
                        { new ("Midgard", 100, 100, TimeSpan.FromSeconds(70)) });

            string json = JsonConvert.SerializeObject(data);
            
            UnityWebRequest uwr = new (_uri, "POST");
            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            yield return uwr.SendWebRequest();
            
            Debug.Log($"Throw Completed, Send session data: {json}");
            
            if (uwr.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log($"Error While Sending: {uwr.error}");
            }
            else
            {
                Debug.Log($"Data Sent: {json}");
            }
        }

        private void OnDisable()
        {
            BaseThrow.ThrowCompleted -= OnThrowCompleted;
        }
    }

    [Serializable]
    public class SessionData
    {
        public string playerId;
        public string username;
        public string timestamp;
        public string gameTime;
        public string flyTime;
        public int flyHeight;
        public string levelName;
        public int sessionCoinsCount;
        public int coinsCount;
        public int hammersCount;
        public int skinsCount;
        public int artifactsCount;
        public List<Score> bestScores;

        public SessionData(string playerId, string username, DateTime timestamp, TimeSpan gameTime, TimeSpan flyTime,
            int flyHeight, string levelName, int sessionCoinsCount, int coinsCount, int hammersCount,
            int skinsCount, int artifactsCount, List<Score> bestScores)
        {
            this.playerId = playerId;
            this.username = username;
            this.timestamp = timestamp.ToUniversalTime().ToString("O", CultureInfo.InvariantCulture);
            this.gameTime = ((int)gameTime.TotalSeconds).ToString();
            this.flyTime = ((int)flyTime.TotalSeconds).ToString();
            this.flyHeight = flyHeight;
            this.levelName = levelName;
            this.sessionCoinsCount = sessionCoinsCount;
            this.coinsCount = coinsCount;
            this.hammersCount = hammersCount;
            this.skinsCount = skinsCount;
            this.artifactsCount = artifactsCount;
            this.bestScores = bestScores;
        }

        [Serializable]
        public class Score
        {
            public string levelName;
            public int height;
            public int coins;
            public string flyTime;

            public Score(string levelName, int height, int coins, TimeSpan flyTime)
            {
                this.levelName = levelName;
                this.height = height;
                this.coins = coins;
                this.flyTime = ((int)flyTime.TotalSeconds).ToString();
            }
        }
    }
}