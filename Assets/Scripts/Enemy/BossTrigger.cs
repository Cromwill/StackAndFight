using UnityEngine;

public class BossTrigger : Interactable
{
    [SerializeField] private Boss _boss;

    public override void Interact(Player player)
    {
        if(_boss.Level < player.LevelSystem.Level)
        {
            player.KillBoss();
        }
    }
}
