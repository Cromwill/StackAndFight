using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GemsRotator : MonoBehaviour
{
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 0), _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
