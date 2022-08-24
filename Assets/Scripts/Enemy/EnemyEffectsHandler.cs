using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectsHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathEffect;

    public void PlayDeathEffect()
    {
        _deathEffect.Play();
    }
}
