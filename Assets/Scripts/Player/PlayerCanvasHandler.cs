using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasHandler : MonoBehaviour
{
    [SerializeField] private Canvas _levelCanvas;

    public void Disable()
    {
        _levelCanvas.gameObject.SetActive(false);
    }
}
