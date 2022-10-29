using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    #region SerializeFields
    [SerializeField]
    private Spawnable _spawnablePref;
    [SerializeField]
    private int _objectsCount;
    [SerializeField]
    private Bounds2D _bounds2D;
    #endregion

    private List<Spawnable> _spawnables = new List<Spawnable>();
    private List<Spawnable> _incorrectSpawnables = new List<Spawnable>();

    private void Start()
    {
        SpawnObjects(_spawnablePref, _objectsCount, _bounds2D);
    }

    //private void FixedUpdate()
    //{
    //    _incorrectSpawnables.Clear();
    //    _incorrectSpawnables.AddRange(_spawnables.Where(obj => obj.CheckIntersection()));

    //    foreach (var obj in _incorrectSpawnables)
    //    {
    //        _spawnables.Remove(obj);
    //        Destroy(obj.gameObject);
    //        SpawnObject(obj, _bounds2D);
    //    }
    //}

    private void SpawnObjects(Spawnable spawnable, int count, Bounds2D bounds)
    {
        //Add to pool?
        for (int i = 0; i < count; i++)
        {
            SpawnObject(spawnable, bounds);
        }
    }

    private void SpawnObject(Spawnable spawnable, Bounds2D bounds)
    {
        Spawnable newObject = Instantiate(spawnable, transform);
        newObject.transform.position = bounds.GetRandomPointInside();
        //_spawnables.Add(newObject);

        Physics.autoSimulation = false;
        Physics.Simulate(Time.fixedDeltaTime * 10);
        Physics.autoSimulation = true;

        if (newObject.CheckIntersection())
        {
            Destroy(newObject.gameObject);
            SpawnObject(spawnable, bounds);
        }
    }
}