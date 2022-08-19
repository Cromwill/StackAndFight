using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCellAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _duration;
    [SerializeField] private float _height;

    public void Trigger()
    {
        StartCoroutine(Animating());
    }

    private IEnumerator Animating()
    {
        float elapsedTime = 0;

        Vector3 startPosition = transform.position;
        
        while (elapsedTime < _duration)
        {
            transform.position = startPosition + Vector3.up *_curve.Evaluate(elapsedTime / _duration) * _height;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
