using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSystem
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    public int Level { get; private set; } 

    public event Action<int> LevelChanged;

    public void IncreaseLevel(int value)
    {
        Level += value;
        _skinnedMeshRenderer.SetBlendShapeWeight(0, Level);
        LevelChanged?.Invoke(Level);
    }
}
