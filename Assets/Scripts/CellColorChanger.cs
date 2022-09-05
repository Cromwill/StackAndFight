using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PathCellRenderer;

public class CellColorChanger : MonoBehaviour
{
    [SerializeField] private CellColor _color;

    private void Start()
    {
        PathCellRenderer[] cellRenderers = FindObjectsOfType<PathCellRenderer>();

        foreach(var cellRenderer in cellRenderers)
        {
            cellRenderer.ChooseColor(_color);
        }
    }
}
