using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpgrade : Upgrade
{
    [SerializeField] private int _lvlPerUpgrade;

    public override void Buy()
    {
        Player.LevelSystem.UpgradeLevels(_lvlPerUpgrade);
        base.Buy();
    }

    protected override void OnInitilize()
    {
        UpgradeName = UpgradeName.Level;
        Value = Player.LevelSystem.AdditionalLevel;
        CostHandler = new ValueHandler(_startCost, 10000, $"{_upgradeType}SaveWord");
    }
}
