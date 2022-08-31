using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.SwitchToPressedState();
    }
}
