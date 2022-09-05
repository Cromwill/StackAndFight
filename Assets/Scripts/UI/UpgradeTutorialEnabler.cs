using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTutorialEnabler : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Canvas _blacScreen;
    [SerializeField] private float _delay;
    [SerializeField] private SwipeHandler _swipeHandler;


    private void Start()
    {
        StartCoroutine(Delay(_delay));
    }

    private IEnumerator Delay(float delay)
    {
        _swipeHandler.Disable();

        yield return new WaitForSeconds(delay);

       _canvas.gameObject.SetActive(true);
       _blacScreen.gameObject.SetActive(true);
    }
}
