using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTutorialEnabler : MonoBehaviour
{
    [SerializeField] private Background _background;
    [SerializeField] private float _delay;
    [SerializeField] private SwipeHandler _swipeHandler;


    private void Start()
    {
        _background = FindObjectOfType<Background>();
        _swipeHandler = FindObjectOfType<SwipeHandler>();
        StartCoroutine(Delay(_delay));
    }

    private IEnumerator Delay(float delay)
    {
        _swipeHandler.Disable();

        yield return new WaitForSeconds(delay);

       _background.gameObject.SetActive(true);
    }
}
