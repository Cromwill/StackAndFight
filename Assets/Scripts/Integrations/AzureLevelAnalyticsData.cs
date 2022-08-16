using System;
using UnityEngine;

[Serializable]
public class AzureLevelAnalyticsData
{
    [SerializeField] private LevelDifficulty _levelDifficulty;
    [SerializeField] private LevelType _levelType;

    public string GetLevelDifficulty()
    {
        return _levelDifficulty switch
        {
            LevelDifficulty.Easy => "easy",
            LevelDifficulty.Normal => "normal",
            LevelDifficulty.Hard => "hard",
        };
    }

    public string GetLevelType()
    {
        return _levelType switch
        {
            LevelType.LevelType1 => "LevelType1",
            LevelType.LevelType2 => "LevelType2",
        };
    }
}

public enum LevelDifficulty
{
    Easy,
    Normal,
    Hard
}

public enum LevelType
{
    LevelType1,
    LevelType2,
}
