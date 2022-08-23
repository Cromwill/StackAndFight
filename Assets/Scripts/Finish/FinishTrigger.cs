using UnityEngine;
using Cinemachine;

public class FinishTrigger : Interactable
{
    [SerializeField] private PathPoint _finalPathPoint;
    [SerializeField] private CinemachineVirtualCamera _wallsCamera;

    public override void Interact(Player player)
    {
        player.Mover.MoveToFinish(_finalPathPoint);
        _wallsCamera.transform.SetParent(player.transform);
        _wallsCamera.Priority = 2;
        Time.timeScale = 0.5f;
    }
}
