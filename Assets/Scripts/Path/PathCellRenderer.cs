using UnityEngine;

public class PathCellRenderer : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Color _activatedColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Colorize()
    {
        _meshRenderer.material.color = _activatedColor;
        //_spriteRenderer.color = _activatedColor;
    }
}
