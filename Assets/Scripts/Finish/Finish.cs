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
        //_levelStep = 20;
        //_enemyInitializer.Init();

        //_finishWalls = GetComponentsInChildren<FinishWall>();

        //var maxLevel = _enemyInitializer.EnemyLevels;

        //_levelStep = maxLevel / _finishWalls.Length;
        //_levelStep -= _levelStep % 10;

        //int currentLevel = maxLevel - _finishWalls.Length*_levelStep;

        //foreach (var finishWall in _finishWalls)
        //{
        //    currentLevel += _levelStep;
        //    int value = currentLevel - (currentLevel % 10);
        //    value = Mathf.Clamp(value, 5, 1000);
        //    finishWall.Init(value, Mathf.Clamp(maxLevel, 0, 500));
        //}
    }
}
