using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private ButtonAnimator _buttonAnimator;
    [SerializeField] private ButtonRenderer _buttonRenderer;
    [SerializeField] private TrapWall _trapWall;

    private bool _isPressed;

    public bool isPressed => _isPressed;

    public void SwitchToPressedState()
    {
        _isPressed = true;
        _buttonAnimator.PlayPress();
        _buttonRenderer.ChangeToPressed();
        _trapWall.Enable();
        print("press");
    }

    public void SwitchToUnpressedState()
    {
        _isPressed = false;
        _buttonAnimator.PlayRevert();
        _buttonRenderer.ChangeToUnpressed();
        _trapWall.Disable();
        print("revert");
    }
}
