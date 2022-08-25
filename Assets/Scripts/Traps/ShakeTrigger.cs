using DG.Tweening;
using UnityEngine;

public class ShakeTrigger : Interactable
{
    [SerializeField] private GameObject _bouncePad;

    public override void Interact(Player player)
    {
        _bouncePad.transform.DOShakeScale(0.5f);
        print("interact");
    }
}
