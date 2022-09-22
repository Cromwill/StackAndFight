using UnityEngine.EventSystems;
using UnityEngine;

public class SwipeHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Player _player;

    private StartViewDisabler _disabler;
    
    private void Awake()
    {
        _disabler = FindObjectOfType<StartViewDisabler>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("sd");
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

        if(_disabler != null)
            _disabler.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void Disable()
    {
        enabled = false;
    }

    public void Enable()
    {
        enabled = true;
    }
}

public enum SwipeDirection
{
    Forward,
    Back,
    Left,
    Right,
    None
}
