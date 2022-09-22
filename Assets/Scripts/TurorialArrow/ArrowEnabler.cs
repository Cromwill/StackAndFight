using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnabler : Interactable
{
    [SerializeField] private GameObject _arrow;
    public override void Interact(Player player)
    {
        _arrow.SetActive(true);
    }
}
