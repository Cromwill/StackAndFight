using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDisabler : Interactable
{
    [SerializeField] private GameObject _arrow;

    public override void Interact(Player player)
    {
        _arrow.SetActive(false);
    }
}
