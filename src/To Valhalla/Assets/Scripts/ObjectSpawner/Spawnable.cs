using NaughtyAttributes;
using UnityEngine;

public abstract class Spawnable : MonoBehaviour
{
    [SerializeField]
    private Collider2D _collider2D;

    [SerializeField]
    private SpawnableType _spawnableType;

    [SerializeField]
    private LayerMask _avoidLayers;

    [Button]
    public bool CheckIntersection()
    {
       RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, _collider2D.bounds.size, 0, Vector2.zero, 1, _avoidLayers);

        foreach (var hit in hits)
        {
            if (hit.collider != _collider2D)
                return true;
        }

        return false;
    }
}

public enum SpawnableType
{ 
    Coin,
    Obstacle,
    Artifact
}