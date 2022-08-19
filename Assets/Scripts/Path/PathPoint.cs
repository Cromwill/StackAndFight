using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    private List<PathPointData> _pathPointDatas = new List<PathPointData>();

    public Vector3 Position => transform.position + Vector3.up*1.5f;

    public void Init(List<PathPointData> pathPointDatas)
    {
        _pathPointDatas = pathPointDatas;
    }

    public bool TryGetPathPoint(SwipeDirection swipeDirection, out PathPoint pathPoint)
    {
        pathPoint = null;

        foreach (var pathPointData in _pathPointDatas)
        {
            if (pathPointData.SwipeDirection == swipeDirection)
            {
                pathPoint = pathPointData.PathPoint;

                return true;
            }
        }

        return false;
    }

    public bool CanGoFrom(SwipeDirection swipeDirection)
    {
        return PathData.OppositeDirectionPairs.TryGetValue(swipeDirection, out Vector3 direction) && HaveGround(direction);
    }

    public bool HaveGround(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction + Vector3.down, out RaycastHit hit) && hit.transform.TryGetComponent(out Path path);
    }
}
