using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class FollowDelayer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Player _player;
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);

        transform.DOMoveZ(-10f, _delay);

        yield return new WaitForSeconds(_delay);

        _camera.Follow = _player.transform;
    }

}
