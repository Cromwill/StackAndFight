using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointData
{
    public PathPoint PathPoint { get; private set; }
    public SwipeDirection SwipeDirection { get; private set; }

    public void SetPathPoint(PathPoint pathPoint)
    {
        PathPoint = pathPoint;
    }

    public void SetSwipDirection(SwipeDirection swipeDirection)
    {
        SwipeDirection = swipeDirection;
    }
}
