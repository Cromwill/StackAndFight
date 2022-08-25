using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _jumpTime;
    [SerializeField] private BoxCollider _finishCollider;

    private bool _canMove = true;
    private bool _isMoving;
    private Coroutine _coroutine;
    private PathPoint _currentPathPoint;
    private const float _minDistance = 0.01f;
    private float _initialY;
    private PathPoint _previousPathPoint;
    private PlayerAnimator _animator;
    private JumpAttack _jumpAttack;

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

    public void Init(PlayerAnimator playerAnimator, LevelSystem levelSystem)
    {
        _animator = playerAnimator;
        _jumpAttack = new JumpAttack(levelSystem);
    }

    public void DisableMovement()
    {
        _canMove = false;
        StopMoving();
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
        if (_isMoving || _canMove == false)
            return;

        if(_currentPathPoint.TryGetPathPoint(swipeDirection, out PathPoint pathPoint))
        {
            Vector3 direction = (pathPoint.Position - transform.position).normalized;
            CheckDistance(direction);
            _previousPathPoint = _currentPathPoint;
            _currentPathPoint = pathPoint;
            _coroutine =  StartCoroutine(MovingTo(pathPoint.Position));

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            _animator.TriggerHeadRun();
        }
    }

    public void MoveToFinish(PathPoint pathPoint)
    {
        if (_isMoving || _canMove == false)
            StopCoroutine(_coroutine);

        Vector3 direction = (pathPoint.Position - transform.position).normalized;
        _previousPathPoint = _currentPathPoint;
        _currentPathPoint = pathPoint;
        _coroutine = StartCoroutine(MovingTo(pathPoint.Position));

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    public void MoveBack()
    {
        if (_previousPathPoint == null || _canMove == false)
            return;

        StopMoving();
        _currentPathPoint = _previousPathPoint;
        _animator.TriggerRun();
        _coroutine = StartCoroutine(MovingTo(_previousPathPoint.Position));

        transform.rotation *= Quaternion.Euler(0, 180f, 0);
    }

    public void Jump(PathPoint pathPoint)
    {
        //transform.position = pathPoint.Position;//доделать: телепортировть после анимации
        StartCoroutine(Jumping(pathPoint));

        _previousPathPoint = pathPoint;
        _currentPathPoint = pathPoint;
    }

    public void EnableFinishCollider()
    {
        _finishCollider.enabled = true;
    }

    public void DisableFinishCollider()
    {
        _finishCollider.enabled = false;
    }

    private IEnumerator Jumping(PathPoint pathPoint)
    {
        _isMoving = true;

        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        _animator.JumpAnimation();

        while (elapsedTime <= _jumpTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, pathPoint.Position, elapsedTime/ _jumpTime) + Vector3.up* _animationCurve.Evaluate(elapsedTime/ _jumpTime) *2f;

            yield return null;
        }

        _isMoving = false;
        _animator.DisableJump();
        _jumpAttack.Perform(transform);
    }

    private IEnumerator MovingTo(Vector3 targetPosition)
    {
        _isMoving = true;

        while (Vector3.Distance(targetPosition, transform.position) > _minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

             yield return null;
        }

        _isMoving = false;
        EnoughDistance = false;
        _animator.TriggerIdle();
    }

    public void StopMoving()
    {
        StopCoroutine(_coroutine);
        _isMoving = false;
    }

    public void DecreaseSpeed()
    {
        _speed /= 2;
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
