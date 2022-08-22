using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private RagdollHandler _ragdollHandler;
    [SerializeField] private Collider _collider;
    [SerializeField] private EnemyLevelCounter _levelCounter;
    [SerializeField] private Shield _shield;
    [SerializeField] private EnemyRotation _rotation;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyRender _enemyRender;

    private Player _player;

    public EnemyAnimator EnemyAnimator => _animator;
    public RagdollHandler RagdollHandler => _ragdollHandler;
    public int Level => _level;

    public event Action<Enemy> Died;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _rotation.Init(_player);
    }

    private void Update()
    {
        if (_player.LevelSystem.Level < _level)
        {
            _enemyRender.SetDefaultd();
            _animator.SetAggresive();
        }
        else
        {
            _animator.SetDefault();
            _enemyRender.SetScared();
        }
    }

    public void Push(Vector3 direction)
    {
        if (_shield != null)
            _shield.Drop();

        _collider.enabled = false;
        _levelCounter.gameObject.SetActive(false);
        RagdollHandler.ActivateRagdoll();
        RagdollHandler.Chest.Push(direction);
        _rotation.Disable();
        Died?.Invoke(this);
    }

    public void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}
