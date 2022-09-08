using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class Currency : Interactable
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private bool _isDecorative;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public override void Interact(Player player)
    {
        if (_isDecorative)
            return;

        SoundHandler.Instance.PlayGemSound();
        player.IncreaseMoney(1);
        _particleSystem.Play();
        _particleSystem.transform.parent = null;
        gameObject.SetActive(false);
    }
}
