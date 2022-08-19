using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _level;

    public int Level => _level;
    public event Action<Enemy> Died;

    public void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}
