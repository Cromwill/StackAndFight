using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Finish : MonoBehaviour
{
    [SerializeField] private int _levelStep;
    [SerializeField] private EnemyInitializer _enemyInitializer;

    private FinishWall[] _finishWalls;

    private void Start()
    {
        _enemyInitializer.Init();

        _finishWalls = GetComponentsInChildren<FinishWall>();

        int currentLevel = _enemyInitializer.EnemyLevels - _finishWalls.Length*_levelStep;

        foreach (var finishWall in _finishWalls)
        {
            currentLevel += _levelStep;
            int value = currentLevel - (currentLevel % 10);
            finishWall.Init(value);
        }
    }
}
