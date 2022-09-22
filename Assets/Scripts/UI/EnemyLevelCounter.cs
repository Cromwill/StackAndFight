using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLevelCounter : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TMP_Text _level;
    //[SerializeField] private TMP_FontAsset _greenFont;
    //[SerializeField] private TMP_FontAsset _redFont;
    [SerializeField] private Color _green;
    [SerializeField] private Color _red;
    [SerializeField] private Image _image;

    private void Update()
    {
        _level.text = $"{_enemy.Level}";
    }

    public void ChangeToGreen()
    {
        _image.color = _green;
    }

    public void ChangeToRed()
    {
        _image.color= _red;
    }
}
