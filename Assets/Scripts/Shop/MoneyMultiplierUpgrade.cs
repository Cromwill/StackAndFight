using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoneyMultiplierUpgrade : Upgrade
{
    [SerializeField] private float _multiplierPerUpgrade;

    public override void Buy()
    {
        Player.MoneyMultiplier.Increase(_multiplierPerUpgrade);
        base.Buy();
    }

    protected override void OnInitilize()
    {
        UpgradeName = UpgradeName.Money;
        Value = Player.MoneyMultiplier;
        CostHandler = new ValueHandler(_startCost, 10000, $"{_upgradeType}SaveWord");
    }
}
