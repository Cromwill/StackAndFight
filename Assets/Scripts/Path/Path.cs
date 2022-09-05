using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private PathCellRenderer _pathCellRenderer;
    [SerializeField] private PathCellAnimation _cellAnimation;

    private bool _isActivated;

    public bool IsActivated => _isActivated;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player) && _isActivated == false)
            Activate();
    }

    public void Activate()
    {
        _isActivated = true;
        _pathCellRenderer.Colorize();
        _cellAnimation.Trigger();
        
    }
}
