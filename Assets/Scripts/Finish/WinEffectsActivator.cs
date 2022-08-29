using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffectsActivator : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _confettis;
    [SerializeField] private float _dealy;

    public void Activate()
    {
        StartCoroutine(Activating(_dealy));
    }

    private IEnumerator Activating(float delay)
    {
        foreach(var confetti in _confettis)
        {
            confetti.Play();

            yield return new WaitForSeconds(delay);
        }
    }
}
