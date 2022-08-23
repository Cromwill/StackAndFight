using UnityEngine;

public class KickTrigger : Interactable
{
    [SerializeField] private CameraSwitcher _cameraSwitcher;

    public override void Interact(Player player)
    {
        _cameraSwitcher.ChangeCamera();
        player.Mover.DecreaseSpeed();
        Time.timeScale = 0.5f;
        player.PlayerAnimator.PlayKick();
    }
}
