using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _image;

    private Upgrade _upgrade;

    public void Init(Upgrade upgrade)
    {
        _cost.text = $"{upgrade.CostHandler.Value}";
        _name.text = $"{upgrade.UpgradeName}";
        _image.sprite = upgrade.Sprite;
        _upgrade = upgrade; 
    }

    private void UpdateInfo()
    {

    }
}
