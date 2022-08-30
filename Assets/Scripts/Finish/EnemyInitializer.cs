using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private Boss _boss;

    private Enemy[] _enemies;
    private Player _player;

    public int EnemyLevels { get; private set; }

    public void Init()
    {
        _enemies = FindObjectsOfType<Enemy>();
        _player = FindObjectOfType<Player>();

        foreach (var enemy in _enemies)
        {
            enemy.Init(_player, 0);

            if(enemy is Boss == false)
                EnemyLevels += enemy.Level;
        }

        _boss.Init(_player, (int)(EnemyLevels *0.9f));
    }
}
