using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _) && other.isTrigger)
        {
            if (_button.isPressed)
                _button.SwitchToUnpressedState();
            else
                _button.SwitchToPressedState();
        }
    }

}
