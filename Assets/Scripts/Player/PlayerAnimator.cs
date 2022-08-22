using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Run = "Run";
    private const string HeadRun = "HeadRun";
    private const string Idle = "Idle";

    public void TriggerRun()
    {
        _animator.SetTrigger(Run);
    }

    public void TriggerHeadRun()
    {
        _animator.SetTrigger(HeadRun);
    }

    public void TriggerIdle()
    {
        _animator.SetTrigger(Idle);
        _animator.ResetTrigger(HeadRun);
        _animator.ResetTrigger(Run);
    }

}
