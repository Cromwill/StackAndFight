using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private int _level;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    public PlayerMover Mover => _mover;

    public event Action<int> LevelChanged;

    private void Awake()
    {
        _mover.Init(_playerAnimator);
    }

    private void Start()
    {
        LevelChanged?.Invoke(_level);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            if(enemy.Level <= _level)
            {
                IncreaseLevel(enemy.Level);
                //enemy.Die();

                PushEnemy(enemy);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void PushEnemy(Enemy enemy)
    {
        float forceValue = 0;
        forceValue = Mathf.Clamp(forceValue, 100, 200);

        Vector3 veloctiy = (transform.forward + transform.up)* forceValue;
        enemy.Push(veloctiy);
    }

    private void IncreaseLevel(int value)
    {
        _level += value;
        _skinnedMeshRenderer.SetBlendShapeWeight(0, _level);
        LevelChanged?.Invoke(_level);
    }
}
