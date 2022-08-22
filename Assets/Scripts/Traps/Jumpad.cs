using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpad : MonoBehaviour
{
    [SerializeField] private PathPoint _pathPoint;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.Mover.IsMoving == false)
            player.Mover.Jump(_pathPoint);
    }
}
