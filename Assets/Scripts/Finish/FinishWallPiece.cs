using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FinishWallPiece : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void Push(Vector3 direction)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction *10, ForceMode.VelocityChange);
        _rigidbody.AddTorque(direction *10, ForceMode.VelocityChange);
    }
}
