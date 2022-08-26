using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
   [SerializeField] private Animator _animator;

    private const string Press = "Press";
    private const string RevertPress = "RevertPress";

    public void PlayPress()
    {
        _animator.Play(Press);
    }

    public void PlayRevert()
    {
        _animator.Play(RevertPress);
    }
}
