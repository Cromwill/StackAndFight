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
    }

    public void Buy()
    {
        if (_playerUpgradeSystem.TryBuy(_upgrade.Type))
        {
            UpdateInfo();
            _buttonAnimation.PlayAnimation();
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
}
