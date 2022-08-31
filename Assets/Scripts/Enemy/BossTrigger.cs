using UnityEngine;

public class BossTrigger : Interactable
{
    [SerializeField] private Boss _boss;
    [SerializeField] private WinEffectsActivator _winEffectsActivator;

    public override void Interact(Player player)
    {
        if(_boss.Level < player.LevelSystem.Level)
        {
            player.CanvasHandler.Disable();
            player.KillBoss();
            _winEffectsActivator.Activate();
            print("boss");
        }
    }
}
