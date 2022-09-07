using UnityEngine;

public class UpgradeTutorialDisabler : MonoBehaviour
{
    private Background _background;
    private SwipeHandler _swipeHandler;
    private HandCanvas _handCanvas;
    private StartViewDisabler _startViewDisabler;

    private void Awake()
    {
        _startViewDisabler = FindObjectOfType<StartViewDisabler>();
        _startViewDisabler.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        _swipeHandler = FindObjectOfType<SwipeHandler>();
        _background = FindObjectOfType<Background>();
        _handCanvas = FindObjectOfType<HandCanvas>();

        if(_startViewDisabler.isActiveAndEnabled == false)
            _startViewDisabler.gameObject.SetActive(true);

        if(_handCanvas != null)
            _handCanvas.gameObject.SetActive(false);

        if (_background != null)
            _background.gameObject.SetActive(false);

        if(_swipeHandler != null)
            _swipeHandler.Enable();

        enabled = false;
    }
}
