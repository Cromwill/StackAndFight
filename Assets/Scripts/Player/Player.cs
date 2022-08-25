using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private LevelSystem _levelSystem;

    private bool _isDead;

    public ValueHandler AdditionalLevel { get; private set; } = new ValueHandler(3, 500, "PlayerLevel");
    public ValueHandler MoneyMultiplier { get; private set; } = new ValueHandler(0, 20, "PlayerCurrencyMultiplier");
    public ValueHandler Wallet { get; private set; } = new ValueHandler(0, 10000, "WalletSaveWord");
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public LevelSystem LevelSystem => _levelSystem;
    public PlayerMover Mover => _mover;
    public bool IsDead => _isDead;

    public event Action CameraSwitched;
    public event Action<Player> DeathChecked;

    private void Awake()
    {
        _mover.Init(_playerAnimator, _levelSystem);
        AdditionalLevel.LoadAmount();
        LevelSystem.IncreaseLevel((int)AdditionalLevel.Value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            if(enemy.Level <= LevelSystem.Level)
            {
                //enemy.Die();

                PushEnemy(enemy);

                if(other.TryGetComponent(out Boss boss))
                {
                    StartCoroutine(KillBoss());
                }
            }
        }
    }

    public void Die()
    {
        _playerAnimator.TriggerFall();
        _mover.DisableMovement();
        _isDead = true;
        DeathChecked?.Invoke(this);
    }

    public void PushEnemy(Enemy enemy)
    {
        float forceValue = 0;
        forceValue = Mathf.Clamp(forceValue, 80, 120) + LevelSystem.Level;
        LevelSystem.IncreaseLevel(enemy.Level);

        Vector3 veloctiy = (transform.forward + transform.up)* forceValue;
        enemy.Push(veloctiy);
    }

    private void Activate(Collider[] colliders)
    {
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Path path))
            {
                if (path.IsActivated == false)
                    path.Activate();
            }
        }
    }

    private IEnumerator KillBoss()
    {
        _mover.StopMoving();
        StartCoroutine(AcitvatingAll());

        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;
        DeathChecked?.Invoke(this);
    }

    private IEnumerator AcitvatingAll()
    {
        Collider[] colliders;
        int maxRadius = 5;
        float radius = 1.5f;

        while (radius <= maxRadius)
        {
            colliders = Physics.OverlapSphere(transform.position, radius);
            Activate(colliders);
            radius += 0.5f;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
