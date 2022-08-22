using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public void Init(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Push(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.VelocityChange);
    }
}
