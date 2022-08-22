using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private Player _payer;
    [SerializeField] private TMP_Text _level;

    private void Start()
    {
        OnLevelChanged(_payer.LevelSystem.Level);
    }

    private void OnEnable() => _payer.LevelSystem.LevelChanged += OnLevelChanged;

    private void OnDisable() => _payer.LevelSystem.LevelChanged -= OnLevelChanged;

    private void OnLevelChanged(int value) => _level.text = value.ToString();
}
