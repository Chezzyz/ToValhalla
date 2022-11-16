using UnityEngine;

namespace Level.Spawn
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnablePref;
        [SerializeField] private Vector2Int _objectCellSize;
        [SerializeField] private int _count;
        [SerializeField] private SpawnGridCalculator _spawnGridCalculator;

        public void Spawn()
        {
            _spawnGridCalculator.SpawnObjects(_spawnablePref, Vector2Int.zero, _objectCellSize, _count,
                transform);
        }
    }
}