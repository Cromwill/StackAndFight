using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private ParticleSystem _explosion;

    public void ActivateExplosion()
    {
        _explosion.Play();
    }
}
