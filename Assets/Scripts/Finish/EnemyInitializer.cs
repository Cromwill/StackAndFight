using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private Boss _boss;

    private Enemy[] _enemies;
    private Player _player;

    public int EnemyLevels { get; private set; }

    public void Init()
    {
        var counter =0 ;
        _enemies = FindObjectsOfType<Enemy>();
        _player = FindObjectOfType<Player>();

        _enemies = _enemies.OrderBy(enemy => enemy.InititalLevel).ToArray();

        foreach (var enemy in _enemies)
        {
            enemy.Init(_player, 0, counter);

            if(counter%5 == 0 && SaveSystem.LoadLevelNumber() % 3 == 0)
                enemy.Init(_player, Random.Range(2,4), counter);

            counter++;

            if(enemy is Boss == false)
                EnemyLevels += enemy.Level;


        }

        _boss.Init(_player, (int)(EnemyLevels *0.9f), counter);
    }
}
