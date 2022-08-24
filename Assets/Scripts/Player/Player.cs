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
                    //boss.ActivateDeathEffect();
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

    private void ActivateRow(Collider[] colliders)
    {
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Path>())
            {
                Path path = collider.GetComponent<Path>();

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
    }

    private IEnumerator AcitvatingAll()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f);
        ActivateRow(colliders);

        yield return new WaitForSeconds(0.1f);

        Collider[] colliders2 = Physics.OverlapSphere(transform.position, 2f);
        ActivateRow(colliders2);

        yield return new WaitForSeconds(0.1f);

        Collider[] colliders3 = Physics.OverlapSphere(transform.position, 3f);
        ActivateRow(colliders3);

        yield return new WaitForSeconds(0.1f);

        Collider[] colliders4 = Physics.OverlapSphere(transform.position, 4f);
        ActivateRow(colliders4);

        yield return new WaitForSeconds(0.1f);

        Collider[] colliders5 = Physics.OverlapSphere(transform.position, 5f);
        ActivateRow(colliders5);
    }
}
