using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShakeAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _duration;

    private float _angleAmplitude = 10;
    private bool Animate = true;

    public void Shake()
    {
        StartCoroutine(Shaking());
    }

    public void Stop()
    {
        Animate = false;
    }

    private IEnumerator Shaking()
    {
        float elapsedTime = 0;

        while (Animate)
        {
            while (elapsedTime < _duration)
            {
                transform.localRotation = Quaternion.Euler(Vector3.forward * _animationCurve.Evaluate(elapsedTime / _duration) * _angleAmplitude);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
