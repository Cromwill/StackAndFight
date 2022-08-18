using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private bool _isMoving;
    private Coroutine _coroutine;

    public bool IsMoving => _isMoving;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle _))
        {
            StopMoving();
        }
    }

    public void TryMove(Vector3 direction)
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        Rotate(direction);
        _coroutine = StartCoroutine(Moving(direction));
        _isMoving = true;
    }

    private void StopMoving()
    {
        StopCoroutine(_coroutine);
        _isMoving = false;
    }

    private void Rotate(Vector3 direction)
    {
        print(Quaternion.Euler(direction));
    }

    private IEnumerator Moving(Vector3 direction)
    {
        while(true)
        {
            _rigidbody.MovePosition(transform.position + _speed * Time.deltaTime * direction.normalized);
            //transform.Translate(_speed * Time.deltaTime * direction); 

            yield return null;
        }
    }
}
