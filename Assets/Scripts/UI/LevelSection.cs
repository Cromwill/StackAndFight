using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelSection : MonoBehaviour
{
    [SerializeField] private LevelSectionView _levelSectionView;

    private List<Enemy> _enemies;
    private int _counter;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<Enemy>().ToList();
        _levelSectionView.Fill(0, _enemies.Count);
    }

    private void OnEnable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Died += OnKill;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Died -= OnKill;
        }
    }

    public void OnKill(Enemy enemy)
    {
        enemy.Died -= OnKill;
        _counter++;
        //_enemies.Remove(enemy);

        _counter = Mathf.Clamp(_counter,0, _enemies.Count);
        _levelSectionView.Fill(_counter, _enemies.Count);
    }
}
