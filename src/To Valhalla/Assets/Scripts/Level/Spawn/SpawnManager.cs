using System;
using System.Collections.Generic;
using Services;
using UnityEngine;

namespace Level.Spawn
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private List<ObjectSpawner> _spawners;
        [SerializeField] private LevelSectorsHandler _sectorsHandler;

        private void OnEnable()
        {
            StartSessionHandler.SessionStarted += OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            SpawnAtSector(_sectorsHandler.GetSectors()[0]);
        }

        public void SpawnAtSector(LevelSector sector)
        {
            if(sector.Coordinates.y < 0) return;
            _spawners.ForEach(spawner => spawner.Spawn(sector));
        }
        
        private void OnDisable()
        {
            StartSessionHandler.SessionStarted -= OnSessionStarted;
        }
    }
}