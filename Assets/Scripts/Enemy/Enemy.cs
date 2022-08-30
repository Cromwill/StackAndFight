using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

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
    [SerializeField] private EnemyEffectsHandler _enemyEffectsHandler;
    [SerializeField] private CameraImpulseGenerator _cameraImpulseGenerator;

    private Player _player;
    private bool _isDead;

    public int Cost { get; private set; } = 5;
    public EnemyAnimator EnemyAnimator => _animator;
    public RagdollHandler RagdollHandler => _ragdollHandler;
    public EnemyRotation Rotation => _rotation;
    public int Level { get; private set; }

    public event Action<Enemy> Died;

    private void Update()
    {
        if (_isDead)
            return;

        if (_player.LevelSystem.Level < Level)
        {
            //_enemyRender.SetDefaultd();
            _animator.SetAggresive();
        }
        else
        {
            _animator.SetDefault();
            //_enemyRender.SetScared();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if (player.LevelSystem.Level >= Level)
                player.PushEnemy(this);
        }
    }

    public void Init(Player player, int additonalLevels)
    {
        _player = player;
        _rotation.Init(_player);

        if (this is Boss)
            _level = 0;

        Level = _level + (int)_player.LevelSystem.AdditionalLevel.Value - 2 + additonalLevels;
        Level = Mathf.Clamp(Level, 1, 1000);
    }

    public void Push(Vector3 direction)
    {
        _isDead = true;

        _cameraImpulseGenerator.ShakeCamera();
        _enemyRender.SetDead();
        _enemyEffectsHandler.PlayDeathEffect();

        if (_shield != null)
        {
            _shield.Drop();
        }

        _collider.enabled = false;
        _levelCounter.gameObject.SetActive(false);
        RagdollHandler.ActivateRagdoll();
        RagdollHandler.Chest.Push(direction);
        _rotation.Disable();
        Died?.Invoke(this);
        StartCoroutine(GoingDown());
    }

    public void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);

    }

    private IEnumerator GoingDown()
    {
        float elapsedTime = 0;

        yield return new WaitForSeconds(5f);

        GetComponent<Collider>().enabled = false;
        _ragdollHandler.DeactivateRagdoll();

        while (elapsedTime < 5)
        {
            transform.position += Vector3.down * Time.deltaTime * 5f;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
