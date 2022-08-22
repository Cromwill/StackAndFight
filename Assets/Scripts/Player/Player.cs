using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private LevelSystem _levelSystem;

    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public LevelSystem LevelSystem => _levelSystem;
    public PlayerMover Mover => _mover;

    public event Action CameraSwitched;

    private void Awake()
    {
        _mover.Init(_playerAnimator, _levelSystem);
        LevelSystem.IncreaseLevel(3);
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
                    boss.ActivateExplosion();
                }
            }
        }
    }

    public void Die()
    {
        _playerAnimator.TriggerFall();
        _mover.DisableMovement();
    }

    public void PushEnemy(Enemy enemy)
    {
        float forceValue = 0;
        forceValue = Mathf.Clamp(forceValue, 80, 120) + LevelSystem.Level;
        LevelSystem.IncreaseLevel(enemy.Level);

        Vector3 veloctiy = (transform.forward + transform.up)* forceValue;
        enemy.Push(veloctiy);
    }

    private IEnumerator KillBoss()
    {
        Time.timeScale = 0.3f;
        CameraSwitched?.Invoke();

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1f;
    }
}
