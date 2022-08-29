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
        if (collitder.TryGetComponent(out Player player) && player.Mover.EnoughDistance && player.Mover.IsMovingBack == false)
            Break();
    }

    public void Explode(Vector3 explosionPosition, float explosionForce = 25f)
    {
        _rigidbody.AddExplosionForce(explosionForce, explosionPosition, 2f, 0.2f, ForceMode.Impulse);
        StartCoroutine(GoingDown());
    }

    public void Break()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
    }

    public void Constrain()
    {
        _rigidbody.isKinematic = true;
    }

    public void Shatter(Vector3 center)
    {
        StartCoroutine(Shattering(Vector3.zero));
    }

    private IEnumerator GoingDown()
    {
        float elapsedTime = 0;

        yield return new WaitForSeconds(5f);

        _rigidbody.isKinematic = true;
        GetComponent<Collider>().enabled = false;

        while (elapsedTime < 5)
        {
            transform.position += Vector3.down * Time.deltaTime * 5f;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator Shattering(Vector3 center)
    {
        Vector3 direction = transform.localPosition.normalized;
        float elapsedTime = 0;

        while(elapsedTime < 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, direction, 1 * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
