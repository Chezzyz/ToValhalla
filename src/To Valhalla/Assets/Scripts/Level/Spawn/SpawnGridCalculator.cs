using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Level.Spawn
{
    public class SpawnGridCalculator : MonoBehaviour
    {
        [SerializeField] private Bounds _bounds;
        [SerializeField] private Vector2 _cellPaddings;
        [SerializeField] private Vector2 _minimalCellSize;
        [SerializeField] private Vector2 _rootOffset;

        private Vector2[,] _cellCenters;

        private float CellHalfX => _cellPaddings.x + _minimalCellSize.x / 2;
        private float CellHalfY => _cellPaddings.y + _minimalCellSize.y / 2;


        public List<T> SpawnObjects<T>(T prefab, LevelSector sector, Vector2Int objectCellSize, int count)
            where T : Object
        {
            List<(Vector2Int cell, Vector2 pos)> spawnPoints = CalculateSpawnPositions(sector, objectCellSize).ToList();
            Vector2Int[] objectCells = CalculateObjectCellPositions(objectCellSize);
            List<T> objects = new();

            for (int i = 0; i < count; i++)
            {
                if (spawnPoints.Count == 0) break;

                (Vector2Int cell, Vector2 pos) current = spawnPoints[Random.Range(0, spawnPoints.Count)];
                objects.Add(Instantiate(prefab, current.pos, Quaternion.identity, sector.GetPool()));
                FillCellMap(sector.CellMap, current.cell, objectCells);
                spawnPoints.Remove(current);
            }

            return objects;
        }

        private Vector2[,] GetCellCenters()
        {
            if (_cellCenters is not null) return _cellCenters;

            Vector2Int gridSize = CalculateGridSize(_bounds, _cellPaddings, _minimalCellSize);
            _cellCenters = new Vector2[gridSize.y, gridSize.x]; //y rows and x columns

            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    float xFirst = CellHalfX;
                    float xDelta = 2 * xFirst;
                    float yFirst = CellHalfY;
                    float yDelta = 2 * yFirst;
                    _cellCenters[y, x] = new Vector2(xFirst + xDelta * x - _bounds.extents.x,
                        yFirst + yDelta * y - _bounds.extents.y);
                }
            }

            return _cellCenters;
        }

        public (Vector2Int cell, Vector2 pos)[] CalculateSpawnPositions(LevelSector sector, Vector2Int objectCellSize)
        {
            Vector2[,] cellCenters = GetCellCenters();
            Vector2Int gridSize = new(cellCenters.GetLength(1), cellCenters.GetLength(0));
            sector.SetCellMapSize(gridSize);
            Vector2Int[] objectCells = CalculateObjectCellPositions(objectCellSize);
            List<Vector2Int> possibleCells = new();
            bool[,] localFilledMap = new bool[gridSize.y, gridSize.x];
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    Vector2Int current = new(x, y);
                    if (CheckCellForObjectPlacement(sector.CellMap, localFilledMap, current, objectCells))
                    {
                        possibleCells.Add(current);
                        FillCellMap(localFilledMap, current, objectCells);
                    }
                }
            }

            Vector2 offset = _rootOffset + new Vector2(sector.Coordinates.x * _bounds.extents.x * 2,
                sector.Coordinates.y * _bounds.extents.y * 2);

            return possibleCells
                .Select(cell => (cell, GetCellCenterWithOffset(cellCenters, cell, offset)))
                .Select(cellPosPair => (cellPosPair.cell, CellCenterToObjectCenter(cellPosPair.Item2, objectCellSize)))
                .ToArray();
        }

        private Vector2 GetCellCenterWithOffset(Vector2[,] cellCenters, Vector2Int cell, Vector2 offset)
        {
            Vector2 cellCenter = cellCenters[cell.y, cell.x];
            return new Vector2(cellCenter.x + offset.x, cellCenter.y + offset.y);
        }

        private Vector2 CellCenterToObjectCenter(Vector2 cellCenter, Vector2Int objectCellSize)
        {
            float x = cellCenter.x - CellHalfX + objectCellSize.x * CellHalfX;
            float y = cellCenter.y - CellHalfY + objectCellSize.y * CellHalfY;
            return new Vector2(x, y);
        }

        private void FillCellMap(bool[,] cellMap, Vector2Int cell, Vector2Int[] offsets)
        {
            foreach (var offset in offsets)
            {
                Vector2Int current = cell + offset;
                cellMap[current.y, current.x] = true;
            }
        }

        private bool CheckCellForObjectPlacement(bool[,] cellFilledMap, bool[,] localFilledMap, Vector2Int cellPos,
            Vector2Int[] objectCells)
        {
            int mapWidth = cellFilledMap.GetLength(1);
            int mapHeight = cellFilledMap.GetLength(0);
            foreach (var objectCell in objectCells)
            {
                Vector2Int current = cellPos + objectCell;
                if (current.y >= mapHeight || current.x >= mapWidth ||
                    cellFilledMap[current.y, current.x] || localFilledMap[current.y, current.x])
                {
                    return false;
                }
            }

            return true;
        }

        private Vector2Int[] CalculateObjectCellPositions(Vector2Int cellSize)
        {
            Vector2Int[] positions = new Vector2Int[cellSize.x * cellSize.y];
            for (int y = 0; y < cellSize.y; y++)
            {
                for (int x = 0; x < cellSize.x; x++)
                {
                    positions[(cellSize.x * y) + x] = new Vector2Int(x, y);
                }
            }

            return positions;
        }

        private Vector2Int CalculateGridSize(Bounds bounds, Vector2 cellPadding, Vector2 cellSize)
        {
            float gridUnitsHeight = bounds.max.y - bounds.min.y;
            float gridUnitsWidth = bounds.max.x - bounds.min.x;

            int gridCellHeight = (int)Mathf.Floor(gridUnitsHeight / (cellSize.y + 2 * cellPadding.y));
            int gridCellWidth = (int)Mathf.Floor(gridUnitsWidth / (cellSize.x + 2 * cellPadding.x));

            return new Vector2Int(gridCellWidth, gridCellHeight);
        }
    }
}