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
            _enemy.EnemyAnimator.TriggerKick();
            StartCoroutine(Delay(player, _enemy));
        }
    }

    private IEnumerator Delay(Player player, Enemy enemy)
    {
        yield return new WaitForSeconds(0.025f);

        player.Mover.StopMoving();

        if (enemy is Enemy)
            player.Die();
        else
            player.Fall();
    }
}
