using UnityEngine;

public class CellColorChanger : MonoBehaviour
{
    [SerializeField] private PathCellRenderer.CellColor _color;

    private void Start()
    {
        PathCellRenderer[] cellRenderers = FindObjectsOfType<PathCellRenderer>();

        foreach (var cellrenderer in cellRenderers)
        {
            if(cellrenderer.IsFinishCell == false)
                cellrenderer.ChooseColor(_color);
        }
    }
}
