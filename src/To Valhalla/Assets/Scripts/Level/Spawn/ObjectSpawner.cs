using UnityEngine;

namespace Level.Spawn
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnablePref;
        [SerializeField] private Vector2Int _objectCellSize;
        [SerializeField] private int _count;
        [SerializeField] private SpawnGridCalculator _spawnGridCalculator;

        public void Spawn(LevelSector sector)
        {
            _spawnGridCalculator.SpawnObjects(_spawnablePref, sector, _objectCellSize, _count);
        }
    }
}