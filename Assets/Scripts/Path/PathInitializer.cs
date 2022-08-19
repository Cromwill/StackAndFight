using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathInitializer : MonoBehaviour
{
    private PathPoint[] pathPoints;

    private void Awake()
    {
        pathPoints = FindObjectsOfType<PathPoint>();

        foreach (var pathPoint in pathPoints)
        {
            pathPoint.Init(CreatePathPointData(pathPoint));
        }
    }

    private List<PathPointData> CreatePathPointData(PathPoint pathPoint)
    {
        List<PathPointData> pathPointDatas = new List<PathPointData>();

        foreach (var directionPair in PathData.DirectionPairs)
        {
            if (TryGetPathPoint(pathPoint, directionPair.Key, out PathPointData point))
                pathPointDatas.Add(point);
        }

        return pathPointDatas;
    }


    private bool TryGetPathPoint(PathPoint initialPathPoint, SwipeDirection swipeDirection ,out PathPointData pathPointData)
    {
        RaycastHit[] rayCastHits = null;
        pathPointData = new PathPointData();
        PathPoint pathPoint = null;
        bool founded = false;

        if (PathData.DirectionPairs.TryGetValue(swipeDirection, out Vector3 direction))
            rayCastHits = Physics.RaycastAll(initialPathPoint.Position, direction);

        foreach (var hit in rayCastHits)
        {
            if(hit.transform.TryGetComponent(out pathPoint))
            {
                if (pathPoint.CanGoFrom(swipeDirection) == false)
                    break;

                pathPointData.SetPathPoint(pathPoint);
                pathPointData.SetSwipDirection(swipeDirection);
                founded = true;

            }
        }

        return founded;
    }
}
