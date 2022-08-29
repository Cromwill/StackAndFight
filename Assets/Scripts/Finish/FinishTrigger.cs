using UnityEngine;
using Cinemachine;
using System.Collections;

public class FinishTrigger : Interactable
{
    [SerializeField] private PathPoint _finalPathPoint;
    [SerializeField] private CinemachineVirtualCamera _wallsCamera;

    public override void Interact(Player player)
    {
        player.Mover.MoveToFinish(_finalPathPoint);
        player.Mover.EnableFinishCollider();
        player.Mover.Disable();
        _wallsCamera.transform.SetParent(player.transform);
        _wallsCamera.m_LookAt = player.transform;
        _wallsCamera.Priority = 2;
        //Time.timeScale = 0.5f;
    }
}
