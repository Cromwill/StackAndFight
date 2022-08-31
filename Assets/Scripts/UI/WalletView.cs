using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;

    private Player _player;

    public float CollectedCurrency { get; private set; }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.Wallet.ValueChanged += UpdateInfo;
        _player.Wallet.ValueIncreased += CountCurrency;
        UpdateInfo();
    }

    private void OnEnable()
    {
        if( _player != null)
        {
            _player.Wallet.ValueChanged += UpdateInfo;
            _player.Wallet.ValueIncreased += CountCurrency;
        }
    }

    private void OnDisable()
    {
        _player.Wallet.ValueChanged -= UpdateInfo;
        _player.Wallet.ValueIncreased -= CountCurrency;
    }

    private void CountCurrency(float value, float collectedValue)
    {
        CollectedCurrency += collectedValue;
    }

    private void UpdateInfo()
    {
        _money.text = $"{(int)_player.Wallet.Value}";
    }
}
