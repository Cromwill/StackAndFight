using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishWall : Interactable
{
    [SerializeField] private bool _enableSlowMotion;
    [SerializeField] private WallLevelUI _wallLevelUI;

    private Brick[] _bricks;
    private SlowMotion _slowMotion;

    public int WallLevel { get; private set; }
    public WallLevelUI WallLevelUI => _wallLevelUI;

    private void Awake()
    {
        _bricks = GetComponentsInChildren<Brick>();
    }

    private void Start()
    {
        _slowMotion = FindObjectOfType<SlowMotion>();
    }

    public void Init(int level)
    {
        WallLevel = level;
        _wallLevelUI.UpdateUI(level);
    }

    public override void Interact(Player player)
    {
        if(WallLevel < player.LevelSystem.Level)
            Break(player);
        else
            player.Die();
    }

    private void Break(Player player)
    {
        foreach (var brick in _bricks)
        {
            brick.Break();
            //brick.Explode(player.transform.position, 10f);
        }

        if (_enableSlowMotion)
            _slowMotion.TriggerSlowMotion();
    }
}
