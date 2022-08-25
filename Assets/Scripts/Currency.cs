using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Interactable
{
    [SerializeField] private ParticleSystem _particleSystem;
    public override void Interact(Player player)
    {
        player.IncreaseMoney(1);
        _particleSystem.Play();
        _particleSystem.transform.parent = null;
        gameObject.SetActive(false);
    }
}
