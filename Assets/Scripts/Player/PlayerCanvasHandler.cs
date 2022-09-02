using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerCanvasHandler : MonoBehaviour
{
    [SerializeField] private Canvas _levelCanvas;
    [SerializeField] private TMP_Text _level;

    public void Disable()
    {
        //_levelCanvas.gameObject.SetActive(false);
        _level.DOFade(0, 0.5f);
    }
}
