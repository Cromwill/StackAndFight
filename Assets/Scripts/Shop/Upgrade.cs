using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Upgrade: MonoBehaviour
{
    [SerializeField] private UpgradeType _upgradeType;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _startCost;
    [SerializeField] private float _costPerBuy;

    protected Player Player;
    public ValueHandler CostHandler { get; private set; }
    public ValueHandler Value { get; protected set; }

    public Sprite Sprite => _sprite;
    public UpgradeType Type => _upgradeType;
    public UpgradeName UpgradeName { get; protected set; }

    public void Init(Player player)
    {
        Player = player;
        CostHandler = new ValueHandler(_startCost, 1000, $"{_upgradeType}SaveWord");
        CostHandler.LoadAmount();
        OnInitilize();
    }

    public virtual void Buy()
    {
        CostHandler.Increase(_costPerBuy);
    }

    protected abstract void OnInitilize();
}
