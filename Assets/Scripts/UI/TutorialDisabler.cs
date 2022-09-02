using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisabler : MonoBehaviour
{
   [SerializeField] private Canvas _canvas;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _canvas.gameObject.SetActive(false);
            enabled = false;
        }
    }
}
