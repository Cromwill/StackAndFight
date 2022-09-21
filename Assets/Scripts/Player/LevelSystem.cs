using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSystem
{
    public ValueHandler AdditionalLevel { get; private set; } = new ValueHandler(2, 500, "PlayerLevel");

    public int Level { get; private set; }
    public int CurrentLevel { get; private set; }

    public event Action<int> LevelChanged;
    public event Action<int> DifferenceSet;

    public void Init()
    {
        AdditionalLevel.LoadAmount();
        Level =(int)AdditionalLevel.Value;
        CurrentLevel = Level;
    }

    public void IncreaseLevel(int value)
    {
        Level += value;

        LevelChanged?.Invoke(Level);
        DifferenceSet?.Invoke(value);
    }

    public void UpgradeLevels(int value)
    {
        AdditionalLevel.Increase(value);
        IncreaseLevel(value);
    }
}
