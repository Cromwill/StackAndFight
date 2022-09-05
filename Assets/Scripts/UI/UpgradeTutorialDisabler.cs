using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTutorialDisabler : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Canvas _background;

    public void OnButtonClick()
    {
        _canvas.gameObject.SetActive(false);

        if (_background != null)
            _background.gameObject.SetActive(false);

        enabled = false;
    }
}
