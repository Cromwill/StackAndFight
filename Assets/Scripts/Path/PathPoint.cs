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

        PathData.DirectionPairs.TryGetValue(swipeDirection, out Vector3 direction);
        RaycastHit[] hits = Physics.RaycastAll(Position, direction, 50);

        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent(out PathPoint tempPathPoint))
            {
                if (tempPathPoint.CanGoFrom(swipeDirection) == false || hit.transform.TryGetComponent(out TrapWall trapWall))
                    break;

                pathPoint = tempPathPoint;
            }
        }

        return pathPoint != null;
    }



    public bool CanGoFrom(SwipeDirection swipeDirection)
    {
        return PathData.OppositeDirectionPairs.TryGetValue(swipeDirection, out Vector3 direction) && HaveGround(direction);
    }

    public bool HaveGround(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, out RaycastHit hit, 2f) && hit.transform.TryGetComponent(out Path path);
    }
}
