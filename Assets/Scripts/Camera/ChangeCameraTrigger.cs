using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTrigger : MonoBehaviour
{
    [SerializeField] private CameraSwitcher _cameraSwitcher;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _cameraSwitcher.ChangeCamera();
        }
    }
}
