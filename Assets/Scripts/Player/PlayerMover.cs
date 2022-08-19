using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private bool _isMoving;
    private Coroutine _coroutine;
    private PathPoint _currentPathPoint;
    private const float _minDistance = 0.01f;
    private float _initialY;
    private PathPoint _previousPathPoint;

    public bool EnoughDistance { get; private set; }
    public bool IsMoving => _isMoving;

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.TryGetComponent(out Obstacle _))
        //{
        //    StopMoving();
        //}
    }

    private void Awake()
    {
        _currentPathPoint = GetClosestPathPoint();
        _initialY = transform.position.y;
    }

    public void TryMove(Vector3 direction)
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        Rotate(direction);
        _isMoving = true;
        _coroutine = StartCoroutine(Moving(direction));
    }

    public void Move(SwipeDirection swipeDirection)
    {
        if (_isMoving)
            return;

        if(_currentPathPoint.TryGetPathPoint(swipeDirection, out PathPoint pathPoint))
        {
            Vector3 direction = (pathPoint.Position - transform.position).normalized;
            CheckDistance(direction);
            _previousPathPoint = _currentPathPoint;
            _currentPathPoint = pathPoint;
            _coroutine =  StartCoroutine(MovingTo(pathPoint.Position));

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    public void MoveToFinish(PathPoint pathPoint)
    {
        if (_isMoving)
            StopCoroutine(_coroutine);

        Vector3 direction = (pathPoint.Position - transform.position).normalized;
        _previousPathPoint = _currentPathPoint;
        _currentPathPoint = pathPoint;
        _coroutine = StartCoroutine(MovingTo(pathPoint.Position));

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    public void MoveBack()
    {
        if (_previousPathPoint == null)
            return;

        StopMoving();
        _currentPathPoint = _previousPathPoint;
        _coroutine = StartCoroutine(MovingTo(_previousPathPoint.Position));
    }

    public void Teleport(PathPoint pathPoint)
    {
        transform.position = pathPoint.Position;//доделать: телепортировть после анимации
        _previousPathPoint = pathPoint;
        _currentPathPoint = pathPoint;
    }

    private IEnumerator MovingTo(Vector3 targetPosition)
    {
        _isMoving = true;
        Vector3 nextPosition;
        float distance = Vector3.Distance(targetPosition, transform.position);

        while (Vector3.Distance(targetPosition, transform.position) > _minDistance)
        {
            nextPosition = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            transform.position = nextPosition;

             yield return null;
        }

        _isMoving = false;
        EnoughDistance = false;
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
        while(_isMoving)
        {
            _rigidbody.MovePosition(transform.position + _speed * Time.deltaTime * direction.normalized);
            //transform.Translate(_speed * Time.deltaTime * direction); 

            yield return null;
        }
    }

    private PathPoint GetClosestPathPoint()
    {
        var pathPoints = FindObjectsOfType<PathPoint>();

        PathPoint closestPathPoint = null;
        float distance = float.MaxValue;

        for (int i = 0; i < pathPoints.Length; i++)
        {
            float tempDistance = Vector3.Distance(pathPoints[i].transform.position, transform.position);

            if(tempDistance < distance)
            {
                distance = tempDistance;

                closestPathPoint = pathPoints[i];
            }
        }

        return closestPathPoint;
    }

    private void CheckDistance(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit))
            EnoughDistance = raycastHit.distance > 5f;
    }
}
