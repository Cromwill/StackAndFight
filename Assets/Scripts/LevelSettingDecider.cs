using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettingDecider : MonoBehaviour
{
    [SerializeField] private LevelSetting _levelSetting;

    public LevelSetting LevelSetting => _levelSetting;

    private void Start()
    {
        Time.timeScale = 1;
        SoundHandler.Instance.PlayBackground(_levelSetting);
    }
}

public enum LevelSetting
{
    Blue,
    Orange,
    Purple,
    Green
}
