using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Shield : Interactable
{
    [SerializeField] private ShakeAnimation _shakeAnimation;

    private bool _isBroken;

    public override void Interact(Player player)
    {
        if (_isBroken)
            return;

        SoundHandler.Instance.PlayArrowSound();
        _shakeAnimation.Trigger();
        player.Mover.MoveBack();
        player.LevelSystem.DecreaseLevel(1);
        var mover = FindObjectOfType<CreoTextMover>();
        mover.Move();
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(GoingDown());
        transform.parent = null;
        _isBroken = true;
    }

    private IEnumerator GoingDown()
    {
        float elapsedTime = 0;

        yield return new WaitForSeconds(5f);

        GetComponent<Collider>().enabled = false;

        while (elapsedTime < 5)
        {
            transform.position += Vector3.down * Time.deltaTime * 5f;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
