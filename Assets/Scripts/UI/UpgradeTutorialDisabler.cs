using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTutorialDisabler : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Canvas _background;
    [SerializeField] private SwipeHandler _swipeHandler;

    public void OnButtonClick()
    {
        print("click");

        if(_canvas != null)
        _canvas.gameObject.SetActive(false);

        if (_background != null)
            _background.gameObject.SetActive(false);

        if(_swipeHandler != null)
        _swipeHandler.Enable();

        enabled = false;
    }
}
