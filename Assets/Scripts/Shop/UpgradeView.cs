using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Image _image;
    [SerializeField] private ButtonAnimation _buttonAnimation;
    [SerializeField] private UIAppearance _uiAppearance;
    [SerializeField] private GameObject _veil;
    [SerializeField] private LevelUpReminder _levelUpReminder;

    private Upgrade _upgrade;
    private PlayerUpgradeSystem _playerUpgradeSystem;
    protected bool IsMultiplier;

    public ButtonAnimation ButtonAnimation => _buttonAnimation;
    public UIAppearance UIAppearance => _uiAppearance;

    public void Init(Upgrade upgrade, PlayerUpgradeSystem playerUpgradeSystem)
    {
        _name.text = $"{upgrade.UpgradeName}";
        _image.sprite = upgrade.Sprite;
        _upgrade = upgrade;
        _playerUpgradeSystem = playerUpgradeSystem;
        IsMultiplier = upgrade.Type == UpgradeType.MoneyMultiplier;
        UpdateInfo();
        CheckBuyPossibilty();

        if(_upgrade.CostHandler.Value <= playerUpgradeSystem.Player.Wallet.Value && upgrade.Type == UpgradeType.StartLevel && _levelUpReminder != null)
            _levelUpReminder.Init();
    }

    public void Buy()
    {
        if (_playerUpgradeSystem.TryBuy(_upgrade.Type))
        {
            UpdateInfo();
            _buttonAnimation.PlayAnimation();
            CheckBuyPossibilty();
        }
    }

    private void UpdateInfo()
    {
        _cost.text = $"{_upgrade.CostHandler.Value}";

        if(IsMultiplier == false)
            _value.text = $"{_upgrade.Value.Value}";
        else
            _value.text = $"x{_upgrade.Value.Value}";
    }

    private void CheckBuyPossibilty()
    {
        bool cantBuy = _upgrade.CostHandler.Value > _playerUpgradeSystem.Player.Wallet.Value;

        _veil.SetActive(cantBuy);
    }
}
