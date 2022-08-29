using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Interactable
{
    [SerializeField] public int _maxHitCount;
    [SerializeField] private ShakeAnimation _shakeAnimation;
    [SerializeField] private BoxCollider _boxCollider;

    private int _hitCounter;
    private Brick[] _bricks;

    private void Awake()
    {
        _bricks = GetComponentsInChildren<Brick>();
    }

    public override void Interact(Player player)
    {
        if(player.Mover.EnoughDistance)
            _hitCounter++;

        if (_hitCounter < _maxHitCount)
            OnHit(player);
        else
            Break(player);
    }

    public void OnHit(Player player)
    {
        player.Mover.MoveBack();//доделать: возможно будем вызывать после анимации удара об стену
        _shakeAnimation.Trigger();

        if (player.Mover.EnoughDistance)
        {
            foreach (var brick in _bricks)
                brick.Shatter(transform.position);
        }
    }

    public void Break(Player player)
    {
        _boxCollider.enabled = false;

        foreach (var brick in _bricks)
            brick.Explode(player.transform.position);
    }
}
