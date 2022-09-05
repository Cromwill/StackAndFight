using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PathCellRenderer;

//[ExecuteInEditMode]
public class CellColorChanger : MonoBehaviour
{
    [SerializeField] private CellColor _color;

    private void Start()
    {
        //if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        //    return;

        print("updated");
        PathCellRenderer[] cellRenderers = FindObjectsOfType<PathCellRenderer>();

        foreach (var cellrenderer in cellRenderers)
        {
            cellrenderer.ChooseColor(_color);
        }
    }

    //private void Update()
    //{
    //    if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
    //    {
    //        enabled = false;
    //    }
    //    else
    //    {
    //        print("Updated");
    //        PathCellRenderer[] cellRenderers = FindObjectsOfType<PathCellRenderer>();

    //        foreach (var cellRenderer in cellRenderers)
    //        {
    //            cellRenderer.ChooseColor(_color);
    //        }
    //    }
    //}
}
