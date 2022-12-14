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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_levelUpReminder != null)
            {
                _levelUpReminder.Disable();
            }
        }
    }
    public void Init(Upgrade upgrade, PlayerUpgradeSystem playerUpgradeSystem)
    {
        _name.text = $"{upgrade.UpgradeName}";
        _image.sprite = upgrade.Sprite;
        _upgrade = upgrade;
        _playerUpgradeSystem = playerUpgradeSystem;
        IsMultiplier = upgrade.Type == UpgradeType.MoneyMultiplier;
        UpdateInfo();
        CheckBuyPossibilty();
        _playerUpgradeSystem.Player.Wallet.ValueChanged += CheckBuyPossibilty;

        if(_upgrade.CostHandler.Value <= playerUpgradeSystem.Player.Wallet.Value && upgrade.Type == UpgradeType.StartLevel && _levelUpReminder != null)
            _uiAppearance.AnimationEnded += _levelUpReminder.Init;
    }

    public void Buy()
    {
        if (_playerUpgradeSystem.TryBuy(_upgrade.Type))
        {
            UpdateInfo();
            _buttonAnimation.PlayAnimation();

            if(_levelUpReminder != null)
                _levelUpReminder.Disable();
        }
    }

    public void Hide()
    {
        _uiAppearance.Hide();

        if (_levelUpReminder != null)
            _levelUpReminder.Disable();

        if (_playerUpgradeSystem != null)
            _playerUpgradeSystem.Player.Wallet.ValueChanged -= CheckBuyPossibilty;
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
