using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.Wallet.ValueChanged += UpdateInfo;
        UpdateInfo();
    }

    private void OnEnable()
    {
        if( _player != null )
            _player.Wallet.ValueChanged += UpdateInfo;  
    }

    private void OnDisable()
    {
        _player.Wallet.ValueChanged -= UpdateInfo;
    }

    private void UpdateInfo()
    {
        _money.text = $"{(int)_player.Wallet.Value}";
    }
}
