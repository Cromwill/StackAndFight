using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private RagdollHandler _ragdollHandler;
    [SerializeField] private Collider _collider;
    [SerializeField] private EnemyLevelCounter _levelCounter;
    [SerializeField] private Shield _shield;

    public RagdollHandler RagdollHandler => _ragdollHandler;
    public int Level => _level;
    public event Action<Enemy> Died;

    public void Push(Vector3 direction)
    {
        if (_shield != null)
            _shield.Drop();

        _collider.enabled = false;
        _levelCounter.gameObject.SetActive(false);
        RagdollHandler.ActivateRagdoll();
        RagdollHandler.Chest.Push(direction);
    }

    public void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}
