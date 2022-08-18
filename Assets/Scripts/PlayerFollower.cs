using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z - 20);
    }
}
