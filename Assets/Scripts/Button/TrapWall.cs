using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TrapWall : Interactable
{
    [SerializeField] private ParticleSystem _dustEffect;

    private Coroutine _coroutine;

    public override void Interact(Player player)
    {
        //player.Mover.MoveBack();
    }

    public void Enable()
    {
        gameObject.SetActive(true);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Enabling());
    }

    public void Disable()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Disabling());
    }

    private IEnumerator Enabling()
    {
        transform.DOLocalMoveY(1f, 0.5f);

        yield return null;
    }

    private IEnumerator Disabling()
    {
        transform.DOLocalMoveY(-1f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
