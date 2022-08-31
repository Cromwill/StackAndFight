using UnityEngine;

public class KickTrigger : Interactable
{
    [SerializeField] private CameraSwitcher _cameraSwitcher;

    public override void Interact(Player player)
    {
        _cameraSwitcher.ChangeCamera();
        player.Mover.DisableFinishCollider();
        var boss = FindObjectOfType<Boss>();

        if (boss.Level >= player.LevelSystem.Level)
            return;

        player.Mover.DecreaseSpeed(2f);
        Time.timeScale = 0.5f;
        player.PlayerAnimator.PlayKick();
    }
}
