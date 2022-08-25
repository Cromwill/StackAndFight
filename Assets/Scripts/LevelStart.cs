using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] UpgradeViewHider _upgradeViewHider;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();

        _player.Mover.Moved += OnLevelStart;
    }

    private void OnEnable()
    {
        if (_player != null)
            _player.Mover.Moved += OnLevelStart;
    }

    private void OnDisable()
    {
        _player.Mover.Moved -= OnLevelStart;
    }

    private void OnLevelStart()
    {
        _player.Mover.Moved -= OnLevelStart;
        _upgradeViewHider.Hide();
    }
}
