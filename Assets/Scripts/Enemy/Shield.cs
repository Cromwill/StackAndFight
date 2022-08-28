using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Shield : Interactable
{
    [SerializeField] private ShakeAnimation _shakeAnimation;

    private bool _isBroken;

    public override void Interact(Player player)
    {
        if (_isBroken)
            return;

        _shakeAnimation.Trigger();
        player.Mover.MoveBack();
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        _isBroken = true;
    }
}
