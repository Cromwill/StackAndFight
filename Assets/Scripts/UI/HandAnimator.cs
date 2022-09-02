using DG.Tweening;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private float _duration;

    private void Start()
    {
        //_hand.transform.DOPunchScale(new Vector3(1.5f, 1.5f), _duration).SetLoops(-1);
        Sequence sequence = DOTween.Sequence();
        sequence.SetEase(Ease.Linear);
        sequence.Append(_hand.transform.DOScale(new Vector3(1.5f, 1.5f), _duration));
        sequence.AppendInterval(0.1f);
        sequence.Append(_hand.transform.DOScale(new Vector3(1f, 1f), _duration));
        sequence.SetLoops(-1, LoopType.Restart);
    }
}
