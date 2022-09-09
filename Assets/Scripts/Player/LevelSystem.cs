using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSystem
{
    public ValueHandler AdditionalLevel { get; private set; } = new ValueHandler(2, 500, "PlayerLevel");

    public int Level { get; private set; } 

    public event Action<int> LevelChanged;

    public void Init()
    {
        AdditionalLevel.LoadAmount();
        Level =(int)AdditionalLevel.Value;
    }

    public void IncreaseLevel(int value)
    {
        Level += value;

        LevelChanged?.Invoke(Level);
    }

    public void UpgradeLevels(int value)
    {
        AdditionalLevel.Increase(value);
        IncreaseLevel(value);
    }

    public void DecreaseLevel(int value)
    {
        Level -= value;

        LevelChanged?.Invoke(Level);
    }
}
