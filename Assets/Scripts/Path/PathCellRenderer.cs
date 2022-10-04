using UnityEngine;

public class PathCellRenderer : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private CellColor _color;
    [SerializeField] private Color _blue;
    [SerializeField] private Color _orange;
    [SerializeField] private Color _purple;
    [SerializeField] private Color _red;
    [SerializeField] private Color _green;
    [SerializeField] private Color _birch;
    [SerializeField] private bool _isFinishCell;

    public bool IsFinishCell => _isFinishCell;

    public void ChooseColor(CellColor color)
    {
        _color = color;
    }

    public void Colorize()
    {
        if(_color == CellColor.Blue)
        _meshRenderer.material.color = _blue;

        if (_color == CellColor.Orange)
            _meshRenderer.material.color = _orange;

        if (_color == CellColor.Purple)
            _meshRenderer.material.color = _purple;

        if (_color == CellColor.Red)
            _meshRenderer.material.color = _red;

        if (_color == CellColor.Green)
            _meshRenderer.material.color = _green;

        if (_color == CellColor.Birch)
            _meshRenderer.material.color = _birch;
    }

    public enum CellColor
    {
        Blue,
        Orange,
        Purple,
        Red,
        Green,
        Birch
    }
}
