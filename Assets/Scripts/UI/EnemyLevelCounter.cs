using TMPro;
using UnityEngine;

public class EnemyLevelCounter : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TMP_Text _level;

    private void Start()
    {
        _level.text = _enemy.Level.ToString();
    }
}
