using DG.Tweening;
using UnityEngine;

public class Jumpad : MonoBehaviour
{
    [SerializeField] private PathPoint _pathPoint;
    [SerializeField] protected GameObject _bouncePad;
    [SerializeField] private float _jumpDuration;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.Mover.IsMoving == false)
        {
            player.Mover.Jump(_pathPoint, _jumpDuration);
            _bouncePad.transform.DOShakeScale(1f);
        }
    }
}
