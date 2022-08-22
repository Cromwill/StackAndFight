using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Interactable
{
    [SerializeField] public int _maxHitCount;
    [SerializeField] private ShakeAnimation _shakeAnimation;

    private int _hitCounter;

    public override void Interact(Player player)
    {
        if(player.Mover.EnoughDistance)
            _hitCounter++;

        if (_hitCounter < _maxHitCount)
            OnHit(player);
        else
            Break();
    }

    public void OnHit(Player player)
    {
        player.Mover.MoveBack();//доделать: возможно будем вызывать после анимации удара об стену
        _shakeAnimation.Trigger();
    }

    public void Break()
    {
        
    }
}
