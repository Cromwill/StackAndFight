using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerUpgradeSystem : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();

        foreach (var upgrade in _upgrades)
        {
            upgrade.Init(_player);
        }
    }

    public bool TryBuy(UpgradeType upgradeType)
    {
        Upgrade upgrade = _upgrades.FirstOrDefault(upgrade => upgrade.UpgradeType == upgradeType);

        if (_player.Wallet.TryDecrease(upgrade.CostHandler.Value))
        {
            upgrade.Buy();
        }

        return true;
    }
}

public enum UpgradeType
{
    StartLevel,
    MoneyMultiplier
}

public enum UpgradeName
{
    Level,
    Money
}
