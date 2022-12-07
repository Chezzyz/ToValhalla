using System;
using System.Collections.Generic;
using System.Linq;
using Level.Spawn;
using Player;
using UnityEngine;

namespace Level
{
    public class LevelSectorsHandler : MonoBehaviour
    {
        [SerializeField] private Vector2 _sectorSize;
        [SerializeField] private PlayerTransformController _playerTransform;
        [SerializeField] private SpawnManager _spawnManager;
        [Header("Sectors")] [SerializeField] private Transform _poolsParent;
        [SerializeField] private List<LevelSector> _sectors;
        [SerializeField] private LevelSector _levelSectorPrefab;

        private Vector2Int CurrentSector =>
            new Vector2Int((int)Mathf.Floor((_playerTransform.GetPosition().x + _sectorSize.x / 2) / _sectorSize.x), 
                (int)Mathf.Floor(_playerTransform.GetPosition().y / _sectorSize.y));
        private Vector2Int _currentSubSector;

        public List<LevelSector> GetSectors() => _sectors;

        private void Update()
        {
            if (_currentSubSector == Vector2Int.zero || _currentSubSector != GetPlayersSubSector(_playerTransform, CurrentSector))
            {
                _currentSubSector = GetPlayersSubSector(_playerTransform, CurrentSector);
                OnSubSectorChanged(CurrentSector, _currentSubSector);
            }
        }

        private void OnSubSectorChanged(Vector2Int currentSector, Vector2Int currentSubSector)
        {
            SetOffFarSectors(currentSector, currentSubSector);
            SetOnNearSectors(currentSector, currentSubSector);
        }

        private void SetOnNearSectors(Vector2Int currentSector, Vector2Int currentSubSector)
        {
            LevelSector first = GetOrCreateSector(currentSector + currentSubSector);
            LevelSector second =
                GetOrCreateSector(new Vector2Int(currentSector.x + currentSubSector.x, currentSector.y));
            LevelSector third =
                GetOrCreateSector(new Vector2Int(currentSector.x, currentSector.y + currentSubSector.y));

            foreach (var sector in new List<LevelSector> { first, second, third })
            {
                if (sector.IsFilled) sector.SetState(true);
                else _spawnManager.SpawnAtSector(sector);
            }
        }

        private LevelSector GetOrCreateSector(Vector2Int sectorPos)
        {
            LevelSector sector = _sectors.Find(levelSector => levelSector.Coordinates == sectorPos);
            if (sector is null)
            {
                sector = Instantiate(_levelSectorPrefab, _poolsParent);
                sector.SetCoordinates(sectorPos);
                _sectors.Add(sector);
            }

            return sector;
        }

        private void SetOffFarSectors(Vector2Int currentSector, Vector2Int currentSubSector)
        {
            List<LevelSector> farSectors =
                _sectors.Where(sector => !SectorIsNear(sector, currentSector, currentSubSector)).ToList();
            farSectors.ForEach(sector => sector.SetState(false));
        }

        private static bool SectorIsNear(LevelSector sector, Vector2Int currentSector, Vector2Int currentSubSector)
        {
            Vector2Int pos = sector.Coordinates;
            return pos == currentSector || pos == currentSector + currentSubSector ||
                   pos == new Vector2Int(currentSector.x + currentSubSector.x, currentSector.y) ||
                   pos == new Vector2Int(currentSector.x, currentSector.y + currentSubSector.y);
        }

        private Vector2Int GetPlayersSubSector(PlayerTransformController player, Vector2Int currentSector)
        {
            Vector2 localSectorPos =
                player.GetPosition() - new Vector2(currentSector.x * _sectorSize.x, currentSector.y * _sectorSize.y);

            return new Vector2Int(localSectorPos.x > 0 ? 1 : -1, localSectorPos.y > _sectorSize.y / 2 ? 1 : -1);
        }
    }
}