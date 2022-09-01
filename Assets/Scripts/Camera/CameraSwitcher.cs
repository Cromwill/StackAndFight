using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CinemachineVirtualCamera _camera2;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _camera = FindObjectOfType<CameraLockX>().GetComponent<CinemachineVirtualCamera>();
        _player.CameraSwitched += ChangeCamera;
    }

    private void OnEnable()
    {
        if(_player != null)
            _player.CameraSwitched += ChangeCamera;

    }

    private void OnDisable() => _player.CameraSwitched -= ChangeCamera;

    public void ChangeCamera()
    {
        StartCoroutine(Changing());
    }

    private IEnumerator Changing()
    {
        _camera.Priority = 0;
        _camera2.Priority = 3;

        yield return new WaitForSecondsRealtime(3.5f);

        _camera.Priority = 3;
        _camera2.Priority = 0;
    }
}
