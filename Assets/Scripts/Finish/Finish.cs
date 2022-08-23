using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private int _levelStep;

    private FinishWall[] _finishWalls;

    private void Awake()
    {
        _finishWalls = GetComponentsInChildren<FinishWall>();

        int currentLevel = 0;

        foreach (var finishWall in _finishWalls)
        {
            currentLevel += _levelStep;
            finishWall.Init(currentLevel);
        }
    }
}
