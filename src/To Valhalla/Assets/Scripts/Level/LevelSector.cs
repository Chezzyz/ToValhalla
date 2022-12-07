using UnityEngine;

namespace Level
{
    public class LevelSector : MonoBehaviour
    {
        [SerializeField] private Transform _selfPool;
        
        public bool[,] CellMap { get; private set; }
        public Vector2Int Coordinates { get; private set; }
        public bool IsFilled { get; private set; }
        
        public Transform GetPool() => _selfPool;
        public void SetCoordinates(Vector2Int pos)
        {
            Coordinates = pos;
            name = $"Sector ({pos.x},{pos.y})";
        }

        public void SetCellMapSize(Vector2Int size)
        {
            CellMap = new bool[size.y, size.x];
            IsFilled = true;
        }

        
        public void SetState(bool state) => _selfPool.gameObject.SetActive(state);
        
    }
}