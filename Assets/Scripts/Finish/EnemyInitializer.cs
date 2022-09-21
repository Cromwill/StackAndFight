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
        var level = 1;
        var previousLevel = 0;
        var previousInitialLevel = 0;
        _enemies = FindObjectsOfType<Enemy>();
        _player = FindObjectOfType<Player>();

        _enemies = _enemies.OrderBy(enemy => enemy.InititalLevel).ToArray();

        foreach (var enemy in _enemies)
        {
            if (enemy is Boss)
                continue;

            counter++;


            if (enemy.InititalLevel == previousInitialLevel == false)
            {
                level += previousLevel;
                previousInitialLevel = enemy.InititalLevel;
            }

            if (counter%5 == 0 && SaveSystem.LoadLevelNumber() % 3 == 0)
            {
                enemy.Init(_player, level, 0, counter);
            }
            else
            {
                var additionalLevels = 0;

                if (level > 30)
                    additionalLevels = -Random.Range(5, 11);

                if (level > 200)
                    level /= 3;

                enemy.Init(_player, level, additionalLevels, counter);
            }

            previousLevel = enemy.Level;


            EnemyLevels += enemy.Level;
        }

        _boss.Init(_player,0, (int)(EnemyLevels *0.2f), counter);
    }
}
