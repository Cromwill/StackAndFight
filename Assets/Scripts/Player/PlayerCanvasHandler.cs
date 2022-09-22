using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerCanvasHandler : MonoBehaviour
{
    [SerializeField] private Canvas _levelCanvas;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private Image _bacground; 

    public void Disable()
    {
        //_levelCanvas.gameObject.SetActive(false);
        _level.DOFade(0, 0.5f);
        _bacground.DOFade(0, 0.5f);
    }
}
