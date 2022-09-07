using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delayer : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Background _background;
    [SerializeField] private HandCanvas _handCanvas;

    private SwipeHandler _swipeHandler;
    private const string WasShown = "WasShown";

    private void Start()
    {
        if (PlayerPrefs.HasKey(WasShown))
            return;

        _swipeHandler = FindObjectOfType<SwipeHandler>();
        _swipeHandler.Disable();
        StartCoroutine(Delay(_delay));
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _background.gameObject.SetActive(true);
        _handCanvas.gameObject.SetActive(true);

        PlayerPrefs.SetString(WasShown, WasShown);
    }
}
