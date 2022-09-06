using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Upgrade: MonoBehaviour
{
    [SerializeField] protected UpgradeType _upgradeType;
    [SerializeField] private Sprite _sprite;
    [SerializeField] protected float _startCost;
    [SerializeField] private float _costPerBuy;

    protected Player Player;
    public ValueHandler CostHandler { get; protected set; }
    public ValueHandler Value { get; protected set; }

    public Sprite Sprite => _sprite;
    public UpgradeType Type => _upgradeType;
    public UpgradeName UpgradeName { get; protected set; }

    public void Init(Player player)
    {
        Player = player;
        OnInitilize();
        CostHandler.LoadAmount();
    }

    public virtual void Buy()
    {
        CostHandler.Increase(_costPerBuy);
        Player.OnBuying(_upgradeType);
    }

    protected abstract void OnInitilize();
}
