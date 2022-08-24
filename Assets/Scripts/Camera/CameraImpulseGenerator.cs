using UnityEngine;
using Cinemachine;

public class CameraImpulseGenerator : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _impulseSource;

    public void ShakeCamera()
    {
        _impulseSource.GenerateImpulse();
    }
}
