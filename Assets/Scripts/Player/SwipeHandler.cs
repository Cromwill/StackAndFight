using UnityEngine.EventSystems;
using UnityEngine;

public class SwipeHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Player _player;

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Vector2 delta = eventData.delta;

    //    if(_player.Mover.IsMoving)
    //        return;

    //    if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
    //    {
    //        if(delta.x > 0)
    //            _player.Mover.TryMove(Vector3.right);
    //        else
    //            _player.Mover.TryMove(Vector3.left);
    //    }
    //    else
    //    {
    //        if(delta.y > 0)
    //            _player.Mover.TryMove(Vector3.forward);
    //        else
    //            _player.Mover.TryMove(Vector3.back);
    //    }
    //}

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;

        if (_player.Mover.IsMoving)
            return;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
                _player.Mover.Move(SwipeDirection.Right);
            else
                _player.Mover.Move(SwipeDirection.Left);
        }
        else
        {
            if (delta.y > 0)
                _player.Mover.Move(SwipeDirection.Forward);
            else
                _player.Mover.Move(SwipeDirection.Back);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}

public enum SwipeDirection
{
    Forward,
    Back,
    Left,
    Right
}
