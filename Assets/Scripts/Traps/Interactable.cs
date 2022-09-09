using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && other.isTrigger)
            Interact(player);
    }

    public abstract void Interact(Player player);
}
