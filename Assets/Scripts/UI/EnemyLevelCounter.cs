using TMPro;
using UnityEngine;

public class EnemyLevelCounter : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TMP_Text _level;

    private void Update()
    {
        _level.text = $"{_enemy.Level}";
    }
}
