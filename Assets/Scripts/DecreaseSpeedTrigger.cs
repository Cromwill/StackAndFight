using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseSpeedTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player) && other.isTrigger)
        {
            player.Mover.DecreaseSpeed(1.2f);
        }
    }
}
