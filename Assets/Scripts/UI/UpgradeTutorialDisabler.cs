using UnityEngine;

public class UpgradeTutorialDisabler : MonoBehaviour
{
    private Background _background;
    private SwipeHandler _swipeHandler;
    private HandCanvas _handCanvas;

    public void OnButtonClick()
    {
        _swipeHandler = FindObjectOfType<SwipeHandler>();
        _background = FindObjectOfType<Background>();
        _handCanvas = FindObjectOfType<HandCanvas>();

        if(_handCanvas != null)
            _handCanvas.gameObject.SetActive(false);

        if (_background != null)
            _background.gameObject.SetActive(false);

        if(_swipeHandler != null)
            _swipeHandler.Enable();

        enabled = false;
    }
}
