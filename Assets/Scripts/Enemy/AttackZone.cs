using System.Collections;
using UnityEngine;

public class AttackZone : Interactable
{
    [SerializeField] private Enemy _enemy;

    private bool _isKicked;

    public override void Interact(Player player)
    {
        if (_enemy.Level > player.LevelSystem.Level && _isKicked == false)
        {
            _enemy.Rotation.Disable();
            _isKicked = true;

            if(_enemy.IsShielded == false)
                _enemy.EnemyAnimator.TriggerKick();

            player.Mover.StopMoving();

            player.Fall();
            _isKicked = false;

            //if (_enemy is Boss)    
            //    player.Fall();
            //else
            //    player.Die();
        }
    }
}
