using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisabler : MonoBehaviour
{
   [SerializeField] private Canvas _canvas;
   [SerializeField] private Canvas _background;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _canvas.gameObject.SetActive(false);

            if(_background != null)
                _background.gameObject.SetActive(false);

            enabled = false;
        }
    }
}
