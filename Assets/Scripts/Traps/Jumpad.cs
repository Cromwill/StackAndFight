using DG.Tweening;
using UnityEngine;

public class Jumpad : MonoBehaviour
{
    [SerializeField] private PathPoint _pathPoint;
    [SerializeField] protected GameObject _bouncePad;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.Mover.IsMoving == false)
        {
            player.Mover.Jump(_pathPoint);
            _bouncePad.transform.DOShakeScale(1f);

        }
    }
}
