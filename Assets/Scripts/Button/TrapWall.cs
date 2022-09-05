using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TrapWall : Interactable
{
    [SerializeField] private ParticleSystem _dustEffect;
    [SerializeField] private BoxCollider _boxCollider;

    private Coroutine _coroutine;

    public override void Interact(Player player)
    {
        //player.Mover.MoveBack();
    }

    private void Start()
    {
        _dustEffect.transform.SetParent(null);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _boxCollider.enabled = true;

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
        _dustEffect.Play();
        transform.DOLocalMoveY(2f, 0.5f);

        yield return null;
    }

    private IEnumerator Disabling()
    {
        transform.DOLocalMoveY(-2f, 0.5f);
        _dustEffect.Play();
        _boxCollider.enabled = false;

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
