using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Retranslator : MonoBehaviour
{
    [SerializeField] private SwipeDirection _direction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player) && other.isTrigger)
        {
            player.Mover.StopMoving();
            player.Mover.Move(_direction, true);
            transform.DOShakeScale(1f);
        }
    }
}
