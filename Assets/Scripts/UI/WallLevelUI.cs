using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallLevelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;

    public void UpdateUI(int level)
    {
        _level.text = $"{level}";
    }
}
