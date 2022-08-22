using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CinemachineVirtualCamera _camera2;
    private void OnEnable() => _player.CameraSwitched += ChangeCamera;


    private void OnDisable() => _player.CameraSwitched -= ChangeCamera;

    public void ChangeCamera()
    {
        StartCoroutine(Changing());
    }

    private IEnumerator Changing()
    {
        _camera.Priority = 0;
        _camera2.Priority = 1;

        yield return new WaitForSecondsRealtime(2f);

        _camera.Priority = 1;
        _camera2.Priority = 0;
    }
}
