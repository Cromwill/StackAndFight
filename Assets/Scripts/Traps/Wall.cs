using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Interactable
{
    [SerializeField] public int _maxHitCount;

    private int _hitCounter;

    public override void Interact(Player player)
    {
        if(player.Mover.EnoughDistance)
            _hitCounter++;

        if (_hitCounter < _maxHitCount)
            player.Mover.MoveBack();//доделать: возможно будем вызывать после анимации удара об стену
        else
            Break();
    }

    public void Break()
    {

    }
}
