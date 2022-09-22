using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDesabler : Interactable
{
    [SerializeField] private GameObject _levelTutorial;
    public override void Interact(Player player)
    {
        _levelTutorial.gameObject.SetActive(false);
    }
}
