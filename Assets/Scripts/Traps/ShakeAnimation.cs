using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;

    private const float _amplitude = 0.3f;
    private const float _duration = 0.3f;
    private Coroutine _shakingCoroutine;

    public void Trigger(float duration = _duration, float amplitude = _amplitude)
    {
        if (_shakingCoroutine != null)
            StopCoroutine(_shakingCoroutine);
        
        _shakingCoroutine = StartCoroutine(Shaking(duration, _amplitude));
    }

    private IEnumerator Shaking(float duration, float amplitude)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.right * _animationCurve.Evaluate(elapsedTime/_duration) * amplitude;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
