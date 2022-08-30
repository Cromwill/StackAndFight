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
    public int Level => _level;

    public event Action<Enemy> Died;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _rotation.Init(_player);
    }

    private void Update()
    {
        if (_isDead)
            return;

        if (_player.LevelSystem.Level < _level)
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
            if (player.LevelSystem.Level >= _level)
                player.PushEnemy(this);
        }
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
