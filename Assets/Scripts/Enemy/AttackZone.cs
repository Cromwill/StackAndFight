using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : Interactable
{
    [SerializeField] private Enemy _enemy;

    private bool _isKicked;

    public override void Interact(Player player)
    {
        if (_enemy.Level > player.LevelSystem.Level && _isKicked == false)
        {
            _isKicked = true;
            _enemy.EnemyAnimator.TriggerKick();
            StartCoroutine(Delay(player));
        }
    }

    private IEnumerator Delay(Player player)
    {
        yield return new WaitForSeconds(0.025f);

        player.Die();
        player.Mover.StopMoving();
    }
}
