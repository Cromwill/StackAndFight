using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishWall : Interactable
{
    [SerializeField] private bool _enableSlowMotion;
    [SerializeField] private WallLevelUI _wallLevelUI;
    [SerializeField] private CameraImpulseGenerator _cameraImpulseGenerator;
    [SerializeField] private ParticleSystem _hitEffect;

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
        _cameraImpulseGenerator.ShakeCamera();

        if(WallLevel < player.LevelSystem.Level)
            Break(player);
        else
            player.Fall();
    }

    private void Break(Player player)
    {
        _hitEffect.Play();

        foreach (var brick in _bricks)
        {
            brick.Break();
            brick.Explode(player.transform.position, 10f);
        }

        if (_enableSlowMotion)
            _slowMotion.TriggerSlowMotion();
    }
}
