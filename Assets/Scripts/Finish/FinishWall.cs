using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishWall : Interactable
{
    [SerializeField] private FinishWallPiece _leftPiece;
    [SerializeField] private FinishWallPiece _rightPiece;
    [SerializeField] private bool _enableSlowMotion;

    private SlowMotion _slowMotion;

    private void Start()
    {
        _slowMotion = FindObjectOfType<SlowMotion>();
    }

    public override void Interact(Player player)
    {
        Break(player);
    }

    private void Break(Player player)
    {
        Vector3 additionalDirection = Vector3.forward * 3 + Vector3.up * 2;
        _leftPiece.Push(Vector3.left + additionalDirection);
        _rightPiece.Push(Vector3.right + additionalDirection);

        if (_enableSlowMotion)
            _slowMotion.TriggerSlowMotion();
    }
}
