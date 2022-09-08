using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Cinemachine;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _duration; 
    
    private void Start()
    {
        StartCoroutine(Moving(_duration));
    }

    private IEnumerator Moving(float duration)
    {
        yield return new WaitForSeconds(1f);

        transform.DOMoveZ(-15f, duration);

        yield return new WaitForSeconds(duration);

        _camera.Priority = 0;
    }
}
