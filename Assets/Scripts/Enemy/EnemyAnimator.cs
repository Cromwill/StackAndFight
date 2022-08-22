using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Aggresive = "Aggresive";
    private const string Kick = "Kick";

    public void SetAggresive()
    {
        _animator.SetBool(Aggresive, true);
    }

    public void SetDefault()
    {
        _animator.SetBool(Aggresive, false);
    }

    public void TriggerKick()
    {
        _animator.SetTrigger(Kick);
    }
}
