using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Shield : Interactable
{
    private bool _isBroken;

    public override void Interact(Player player)
    {
        if (_isBroken)
            return;

        player.Mover.MoveBack();
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        _isBroken = true;
    }
}
