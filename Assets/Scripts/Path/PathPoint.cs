using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        hits = hits.OrderBy(hit => hit.distance).ToArray();

        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent(out TrapWall trapWall))
                break;

            if (hit.transform.TryGetComponent(out PathPoint tempPathPoint))
            {
                if (tempPathPoint.CanGoNext(swipeDirection) == false)
                {
                    pathPoint = tempPathPoint;
                    return true;
                }

                //if (tempPathPoint.CanGoFrom(swipeDirection) == false)
                //    break;

                //pathPoint = tempPathPoint;
            }
        }

        return pathPoint != null;
    }



    public bool CanGoFrom(SwipeDirection swipeDirection)
    {
        return PathData.OppositeDirectionPairs.TryGetValue(swipeDirection, out Vector3 direction) && HaveGround(direction);
    }

    public bool CanGoNext(SwipeDirection swipeDirection)
    {
        return PathData.DirectionPairs.TryGetValue(swipeDirection, out Vector3 direction) && HaveGround(direction);
    }

    public bool HaveGround(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, out RaycastHit hit, 1f) && hit.transform.TryGetComponent(out Path path);
    }
}
