using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _currency;
    [SerializeField] private WalletView _walletView;

    private Coroutine _coroutine;

    private const float Duration = 1f;

    private void OnEnable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Counting(_walletView.CollectedCurrency));
    }

    private IEnumerator Counting(float value)
    {
        float textValue = 0;
        float elapsedTime = 0;
        _currency.text = $"{(int)textValue}";

        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;

            textValue = Mathf.Lerp(0, value, elapsedTime/Duration);

            _currency.text = $"x{(int)textValue}";


            yield return null;
        }


    }
}
