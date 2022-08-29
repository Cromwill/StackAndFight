using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Run = "Run";
    private const string HeadRun = "HeadRun";
    private const string Idle = "Idle";
    private const string Jump = "Jump";
    private const string Fall = "Fall";
    private const string Kick = "Kick";

    public void TriggerRun()
    {
        _animator.SetTrigger(Run);
    }

    public void TriggerHeadRun()
    {
        _animator.SetTrigger(HeadRun);
        _animator.ResetTrigger(Idle);
    }

    public void JumpAnimation()
    {
        _animator.SetBool(Jump, true);
    }

    public void DisableJump()
    {
        _animator.SetBool(Jump, false);
    }

    public void TriggerFall()
    {
        _animator.SetTrigger(Fall);
    }

    public void PlayKick()
    {
        _animator.Play(Kick);
    }

    public void TriggerIdle()
    {
        _animator.SetTrigger(Idle);
        _animator.ResetTrigger(HeadRun);
        _animator.ResetTrigger(Run);
    }
}
