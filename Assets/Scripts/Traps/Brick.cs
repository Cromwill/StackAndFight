using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Brick : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider collitder)
    {
        if (collitder.TryGetComponent(out Player player) && player.Mover.EnoughDistance)
            Break();
    }

    public void Explode(Vector3 explosionPosition, float explosionForce = 25f)
    {
        _rigidbody.AddExplosionForce(explosionForce, explosionPosition, 2f, 0.2f, ForceMode.Impulse);
    }

    public void Break()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
    }
}
