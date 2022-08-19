using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private PathPoint _pathPoint;

    private const float _teleportDistance = 0.01f;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.Mover.IsMoving == false)
            Teleportation(player);
    }

    private void Teleportation(Player player)
    {
        player.Mover.Teleport(_pathPoint);
    }
}
