using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialView;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private AnimationCurve _scaleCurve;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            print("trigger");
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
            _tutorialView.transform.localScale = new Vector3(_scaleCurve.Evaluate(time), _scaleCurve.Evaluate(time));
            time += Time.deltaTime;
            yield return null;
        }
    }
}
