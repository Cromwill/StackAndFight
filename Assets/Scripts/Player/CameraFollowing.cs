using UnityEngine;

namespace RunnerMovementSystem.Examples
{
    public class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [Space(15)]
        [SerializeField] private Transform _target;
        [SerializeField] private float _height;
        [SerializeField] private float _distance;
        [SerializeField] private float _offset;
        [SerializeField] private float _lookAngle;
        [SerializeField] private float _lookAngleY;

        private Vector3 _targetPosition;

        private void LateUpdate()
        {
            _targetPosition = _target.position;
            _targetPosition -= Vector3.forward * _distance;
            _targetPosition += Vector3.up * _height;
            _targetPosition += Vector3.right * _offset;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

            var targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            targetRotation.eulerAngles = new Vector3(_lookAngle, _lookAngleY, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
