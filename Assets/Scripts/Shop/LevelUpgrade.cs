using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpgrade : Upgrade
{
    [SerializeField] private int _lvlPerUpgrade;

    private void Awake()
    {
        UpgradeName = UpgradeName.Level;
    }

    public override void Buy()
    {
        Player.AdditionalLevel.Increase(_lvlPerUpgrade);
        base.Buy();
    }
}
