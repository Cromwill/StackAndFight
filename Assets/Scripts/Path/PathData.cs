using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathData : MonoBehaviour
{
    public static IReadOnlyDictionary<SwipeDirection, Vector3> DirectionPairs = new Dictionary<SwipeDirection, Vector3>()
    {
        {SwipeDirection.Left, Vector3.left},
        {SwipeDirection.Right, Vector3.right},
        {SwipeDirection.Forward, Vector3.forward},
        {SwipeDirection.Back, Vector3.back }
    };

    public static IReadOnlyDictionary<Vector3, SwipeDirection> ReverseDirectionPairs = new Dictionary<Vector3, SwipeDirection>()
    {
        {Vector3.left, SwipeDirection.Left},
        {Vector3.right, SwipeDirection.Right},
        {Vector3.forward, SwipeDirection.Forward},
        {Vector3.back, SwipeDirection.Back}
    };

    public static IReadOnlyDictionary<SwipeDirection, Vector3> OppositeDirectionPairs = new Dictionary<SwipeDirection, Vector3>()
    {
        {SwipeDirection.Left, Vector3.right},
        {SwipeDirection.Right, Vector3.left},
        {SwipeDirection.Forward, Vector3.back},
        {SwipeDirection.Back, Vector3.forward }
    };
}
