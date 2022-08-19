using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    private float _slowTime = 0.1f;
    private Coroutine _coroutine;

    public void TriggerSlowMotion()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SlowingMotion());
    }

    private IEnumerator SlowingMotion()
    {
        Time.timeScale = 0.2f;

        yield return new WaitForSeconds(_slowTime);

        Time.timeScale = 1f;
    }
}
