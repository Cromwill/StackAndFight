using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Decal : MonoBehaviour
{
   [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Fade()
    {
        _spriteRenderer.DOFade(0, 2);
    }
}
