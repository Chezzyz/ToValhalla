using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds2D : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _collider2D;

    public Vector3 GetRandomPointInside()
    {
        float width = _collider2D.bounds.size.x;
        float height = _collider2D.bounds.size.y;
        Vector3 resultPoint = _collider2D.bounds.center + new Vector3(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2), 0);

        if (IsInsideBounds(resultPoint))
            return resultPoint;
        else
            return GetRandomPointInside();
    }

    private bool IsInsideBounds(Vector3 point) => _collider2D.bounds.Contains(point);
}
