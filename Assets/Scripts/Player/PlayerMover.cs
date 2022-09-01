using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private BoxCollider _finishCollider;
    [SerializeField] private LayerMask _brickWall;
    [SerializeField] private LayerMask _moveLayer;
    [SerializeField] private DecalSpawner _decalSpawner;
    [SerializeField] private ParticleSystem _runField;

    private bool _canMove = true;
    private bool _isMoving;
    private Coroutine _coroutine;
    private PathPoint _currentPathPoint;
    private const float _minDistance = 0.01f;
    private PathPoint _previousPathPoint;
    private PlayerAnimator _animator;
    private JumpAttack _jumpAttack;


    public bool IsMovingBack { get; private set; }
    public bool EnoughDistance { get; private set; }
    public bool IsMoving => _isMoving;

    public event Action Moved;

    private void Awake()
    {
        _currentPathPoint = GetClosestPathPoint();
    }

    public void Init(PlayerAnimator playerAnimator, LevelSystem levelSystem)
    {
        _animator = playerAnimator;
        _jumpAttack = new JumpAttack(levelSystem);
        Appear();
    }

    public void DisableMovement()
    {
        _canMove = false;
        StopMoving();
    }

    public void Move(SwipeDirection swipeDirection, bool isRetranslator = false)
    {
        if(isRetranslator == false)
        {
            if (_isMoving || _canMove == false)
                return;
        }

        StopMoving();

        Moved?.Invoke();

        if (TryGetPathPoint(swipeDirection, out PathPoint pathPoint))
        {
            Vector3 direction = (pathPoint.Position - transform.position).normalized;
            CheckDistance(direction);
            _previousPathPoint = _currentPathPoint;
            _currentPathPoint = pathPoint;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            _coroutine =  StartCoroutine(MovingTo(pathPoint.Position, swipeDirection));

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

        IsMovingBack = true;
        StopMoving();
        _currentPathPoint = _previousPathPoint;
        _animator.TriggerRun();

        transform.rotation *= Quaternion.Euler(0, 180f, 0);

        _coroutine = StartCoroutine(MovingTo(_previousPathPoint.Position));

    }

    public void Jump(PathPoint pathPoint, float jumpTime)
    {
        StartCoroutine(Jumping(pathPoint, jumpTime));

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

    public void Disable()
    {
        _canMove = false;
    }

    public void PushBack()
    {
        StartCoroutine(PushingBack());
    }

    public void Kick(Vector3 targetPosition)
    {
        StopMoving();
        StartCoroutine(AnimatingMove(targetPosition,1f));
    }

    private void Appear()
    {
        _animator.JumpAnimation();
        Vector3 initialPosition = transform.position;
        transform.position = transform.position + Vector3.up * 5f;
        StartCoroutine(AnimatingMove(initialPosition, 0.2f, true));
    }

    private IEnumerator AnimatingMove(Vector3 targetPosition, float duration, bool isKinematic = false)
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        _runField.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _rigidbody.isKinematic = isKinematic;
        _animator.DisableJump();
    }

    private IEnumerator Jumping(PathPoint pathPoint, float jumpTime)
    {
        _isMoving = true;

        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        _animator.JumpAnimation();

        while (elapsedTime <= jumpTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, pathPoint.Position, elapsedTime/ jumpTime) + Vector3.up* _animationCurve.Evaluate(elapsedTime/ jumpTime) *2f;

            yield return null;
        }

        _isMoving = false;
        _animator.DisableJump();
        _jumpAttack.Perform(transform);
    }

    private IEnumerator MovingTo(Vector3 targetPosition, SwipeDirection swipeDirection = SwipeDirection.None)
    {
        _isMoving = true;
        float distance = Vector3.Distance(targetPosition, transform.position);
        Vector3 nextPosition;
        _runField.Play();

        while (distance > _minDistance)
        {
            if (swipeDirection != SwipeDirection.None && TryGetPathPoint(swipeDirection, out PathPoint pathPoint))
                targetPosition = pathPoint.Position;

            nextPosition = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            if(swipeDirection == SwipeDirection.Left || swipeDirection == SwipeDirection.Right)
                transform.position = new Vector3(nextPosition.x, nextPosition.y, Mathf.RoundToInt(nextPosition.z));

            if (swipeDirection == SwipeDirection.Forward || swipeDirection == SwipeDirection.Back)
                transform.position = new Vector3(Mathf.RoundToInt(nextPosition.x), nextPosition.y, nextPosition.z);

            if (swipeDirection == SwipeDirection.None)
                transform.position = nextPosition;

            distance = Vector3.Distance(targetPosition, transform.position);

            if (swipeDirection != SwipeDirection.None)
            {
                PathData.DirectionPairs.TryGetValue(swipeDirection, out Vector3 direction);
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }

            yield return null;
        }

        _isMoving = false;
        EnoughDistance = false;
        IsMovingBack = false;
        _animator.TriggerIdle();
        _decalSpawner.Spawn();
        _runField.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private IEnumerator PushingBack()
    {
        float speed = 3;
        float timer = 0;
        Vector3 targetPosition = transform.position - transform.forward * 3;

        while (timer < 1f)
        {
            //transform.Translate(speed * Time.deltaTime * Vector3.back);
            transform.position = Vector3.Lerp(transform.position, targetPosition, timer/1f);
            timer += Time.deltaTime;
            yield return null;
        }

        _rigidbody.isKinematic = false;
    }

    public void StopMoving()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _isMoving = false;
    }

    public void DecreaseSpeed(float value)
    {
        _speed /= value;
    }

    public void IncreaseSpeed(float value)
    {
        _speed *= value;
    }

    public bool TryGetPathPoint(SwipeDirection swipeDirection, out PathPoint pathPoint)
    {
        pathPoint = null;

        PathData.DirectionPairs.TryGetValue(swipeDirection, out Vector3 direction);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 50, _moveLayer);
        hits = hits.OrderBy(hit => hit.distance).ToArray();

        foreach (var hit in hits)
        {

            if (hit.transform.TryGetComponent(out TrapWall trapWall))
                break;

            if (hit.transform.TryGetComponent(out PathPoint tempPathPoint))
            {
                if (tempPathPoint.CanGoFrom(swipeDirection) == false)
                    break;

                pathPoint = tempPathPoint;
            }
        }

        return pathPoint != null;
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
        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, 50, _brickWall))
            EnoughDistance = raycastHit.distance > 1f;
    }
}
