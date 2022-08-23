using UnityEngine;

public class FinishTrigger : Interactable
{
    [SerializeField] private PathPoint _finalPathPoint;

    public override void Interact(Player player)
    {
        player.Mover.MoveToFinish(_finalPathPoint);
    }
}
