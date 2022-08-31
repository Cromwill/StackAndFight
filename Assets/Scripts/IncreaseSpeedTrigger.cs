using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && other.isTrigger)
        {
            player.Mover.IncreaseSpeed(1.2f);
        }
    }
}
