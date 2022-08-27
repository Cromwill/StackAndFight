using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TrapWall : Interactable
{
    [SerializeField] private ParticleSystem _dustEffect;

    public override void Interact(Player player)
    {
        //player.Mover.MoveBack();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        //_dustEffect.Play();
        transform.DOLocalMoveY(1f, 0.5f);
    }

    public void Disable()
    {
        StartCoroutine(Disabling());
    }

    private IEnumerator Disabling()
    {
        transform.DOLocalMoveY(-1f, 0.5f);
        //_dustEffect.Play();

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
