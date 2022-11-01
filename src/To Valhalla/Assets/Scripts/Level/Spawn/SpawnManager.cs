using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Spawn
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private List<ObjectSpawner> _spawners;

        private void Start()
        {
            _spawners.ForEach(spawner=> spawner.Spawn());
        }
    }
}