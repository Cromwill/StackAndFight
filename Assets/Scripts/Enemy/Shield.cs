using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Interactable
{
    public override void Interact(Player player)
    {
        player.Mover.MoveBack();
    }
}
