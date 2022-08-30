using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Retranslator : Interactable
{
    [SerializeField] private SwipeDirection _direction;

    public override void Interact(Player player)
    {
        player.Mover.Move(_direction, true);
        transform.DOShakeScale(1f);
    }
}
