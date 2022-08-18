using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private int _level;

    public PlayerMover Mover => _mover;

    public event Action<int> LevelChanged;

    private void Start()
    {
        LevelChanged?.Invoke(_level);    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if(enemy.Level < _level)
            {
                IncreaseLevel(enemy.Level);
                enemy.Die();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void IncreaseLevel(int value)
    {
        _level += value;
        LevelChanged?.Invoke(_level);
    }
}
