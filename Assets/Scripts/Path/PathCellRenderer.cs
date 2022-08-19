using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCellRenderer : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Color _activatedColor;

    public void Colorize()
    {
        _meshRenderer.material.color = _activatedColor;
    }
}
