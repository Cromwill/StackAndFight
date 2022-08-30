using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _levelUpEffect;
    [SerializeField] private ParticleSystem _moneyUpEffect;
    [SerializeField] private ParticleSystem _deathEffect;

    public void PlayLevelUp()
    {
        _levelUpEffect.Play();
    }

    public void PlayMoneyUp()
    {
        _moneyUpEffect.Play();
    }

    public void PlayDeath()
    {
        _deathEffect.Play();
    }
}
