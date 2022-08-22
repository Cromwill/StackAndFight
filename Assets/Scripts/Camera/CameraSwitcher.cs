using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CinemachineVirtualCamera _camera2;

    public void ChangeCamera()
    {
        _camera.Priority = 0;
        _camera2.Priority = 1;
    }
}
