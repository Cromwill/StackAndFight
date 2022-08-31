using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ArrowRender : MonoBehaviour
{
    public void Shake()
    {
        transform.DOShakeScale(1f);
    }
}
