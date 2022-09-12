using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CreoTextMover : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Move()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_text.DOFade(1, 0.5f));
        sequence.Insert(0, transform.DOMoveY(4, 0.5f));
        //sequence.AppendInterval(0.1f);
        sequence.Append(_text.DOFade(0, 0.5f));
        sequence.Append(transform.DOMoveY(3, 0));
    }
}
