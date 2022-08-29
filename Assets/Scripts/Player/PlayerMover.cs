﻿using System;
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

    public event Action Moved;

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

        Moved?.Invoke();

        if (TryGetPathPoint(swipeDirection, out PathPoint pathPoint))
        {
            Vector3 direction = (pathPoint.Position - transform.position).normalized;
            CheckDistance(direction);
            _previousPathPoint = _currentPathPoint;
            _currentPathPoint = pathPoint;
            _coroutine =  StartCoroutine(MovingTo(pathPoint.Position, swipeDirection));

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

        while (distance > _minDistance)
        {
            if (swipeDirection != SwipeDirection.None && TryGetPathPoint(swipeDirection, out PathPoint pathPoint))
                targetPosition = pathPoint.Position;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            distance = Vector3.Distance(targetPosition, transform.position);

            yield return null;
        }

        _isMoving = false;
        EnoughDistance = false;
        _animator.TriggerIdle();
    }

    public void StopMoving()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _isMoving = false;
    }

    public void DecreaseSpeed()
    {
        _speed /= 2;
    }

    public bool TryGetPathPoint(SwipeDirection swipeDirection, out PathPoint pathPoint)
    {
        pathPoint = null;

        PathData.DirectionPairs.TryGetValue(swipeDirection, out Vector3 direction);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 50);
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
        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, 50, _brickWall))
        {
            Debug.Log(raycastHit.distance);
            EnoughDistance = raycastHit.distance > 1f;
        }
    }
}
