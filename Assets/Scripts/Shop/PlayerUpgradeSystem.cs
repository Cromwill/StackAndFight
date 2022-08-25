using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerUpgradeSystem : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private UpgradeView _viewPrefab;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();

        foreach (var upgrade in _upgrades)
        {
            upgrade.Init(_player);
        }

        foreach (var upgrade in _upgrades)
        {
            var view = Instantiate(_viewPrefab, transform);
            view.Init(upgrade, this);
        }
    }

    public bool TryBuy(UpgradeType upgradeType)
    {
        Upgrade upgrade = _upgrades.FirstOrDefault(upgrade => upgrade.Type == upgradeType);

        if (_player.Wallet.TryDecrease(upgrade.CostHandler.Value))
        {
            upgrade.Buy();

            return true;
        }

        return false;
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
