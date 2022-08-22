using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private bool _canRotate;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (_canRotate == false)
            return;

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        direction = Vector3Int.RoundToInt(direction);

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        float currentRotation = transform.localRotation.eulerAngles.y;

        float yRotation = Mathf.Round(currentRotation / 90) * 90f;

        transform.localRotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z);
    }

    public void Disable()
    {
        _canRotate = false;
    }
}
