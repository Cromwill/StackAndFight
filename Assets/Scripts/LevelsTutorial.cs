using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialView;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private float _animationDuration;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _arrow.gameObject.SetActive(true);
            _tutorialView.gameObject.SetActive(true);
            StartCoroutine(ChangingScale());
        }
    }

    private IEnumerator ChangingScale()
    {
        float time = 0;

        while(time < _scaleCurve.keys[_scaleCurve.keys.Length - 1].time)
        {
            _tutorialView.transform.localScale = Vector2.one * _scaleCurve.Evaluate(time / _animationDuration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
