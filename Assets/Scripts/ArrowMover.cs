using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ArrowMover : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _yMovement;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        Move();
    }

    private void Move()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(_yMovement, _duration));
        sequence.AppendInterval(0.25f);
        sequence.Append(transform.DOMoveY(_startPosition.y, _duration));
        sequence.SetLoops(-1, LoopType.Restart);
        sequence.SetEase(Ease.Linear);
    }
}
