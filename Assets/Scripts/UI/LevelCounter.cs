using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private Player _payer;
    [SerializeField] private TMP_Text _level;

    private void OnEnable() => _payer.LevelChanged += OnLevelChanged;

    private void OnDisable() => _payer.LevelChanged -= OnLevelChanged;

    private void OnLevelChanged(int value) => _level.text = value.ToString();
}
