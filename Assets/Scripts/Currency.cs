using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Interactable
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private bool _isDecorative;

    public override void Interact(Player player)
    {
        if (_isDecorative)
            return;

        player.IncreaseMoney(1);
        _particleSystem.Play();
        _particleSystem.transform.parent = null;
        gameObject.SetActive(false);
    }
}
