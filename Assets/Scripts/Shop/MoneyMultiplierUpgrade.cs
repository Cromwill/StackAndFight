using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoneyMultiplierUpgrade : Upgrade
{
    [SerializeField] private float _multiplierPerUpgrade;

    private void Awake()
    {
        UpgradeName = UpgradeName.Money;
    }

    public override void Buy()
    {
        Player.MoneyMultiplier.Increase(_multiplierPerUpgrade);
        base.Buy();
    }
}
