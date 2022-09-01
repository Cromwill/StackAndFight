using UnityEngine;

public class KickTrigger : Interactable
{
    [SerializeField] private CameraSwitcher _cameraSwitcher;
    [SerializeField] private Boss _boss;

    public override void Interact(Player player)
    {
        _cameraSwitcher.ChangeCamera();
        player.Mover.DisableFinishCollider();

        if (_boss.Level >= player.LevelSystem.Level)
            return;

        player.Mover.DecreaseSpeed(2f);
        player.Mover.Kick(_boss.transform.position);
        Time.timeScale = 0.5f;
        player.PlayerAnimator.PlayKick();
    }
}
