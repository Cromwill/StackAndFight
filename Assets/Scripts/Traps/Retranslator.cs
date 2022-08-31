using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Retranslator : MonoBehaviour
{
    [SerializeField] private SwipeDirection _direction;
    [SerializeField] private ArrowRender _arrowRender;

    private bool _isChecking;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player) && other.isTrigger)
        {
            //StartCoroutine(CheckingPlayerPosition(player));
            player.Mover.StopMoving();
            player.Mover.Move(_direction, true);
            _arrowRender.Shake();
        }
    }

    private IEnumerator CheckingPlayerPosition(Player player)
    {
        _isChecking = true;
        float maxTimeChecking = 3f;
        float elapsedTime = 0;
        while (Vector3.Distance(transform.position, player.transform.position) > 0.5f)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log(Vector3.Distance(transform.position, player.transform.position));

            if(elapsedTime < maxTimeChecking)
                yield break;

            yield return null;
        }

        player.Mover.StopMoving();
        player.Mover.Move(_direction, true);
        //transform.DOShakeScale(1f);
        _isChecking = false;
    }
}
