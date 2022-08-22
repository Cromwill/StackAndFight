using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRender : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _scaredMaterial;
    [SerializeField] private GameObject _angry;
    [SerializeField] private GameObject _scared;

    public void SetScared()
    {
        _skinnedMeshRenderer.material = _scaredMaterial;
        _scared.SetActive(true);
        _angry.SetActive(false);
    }

    public void SetDefaultd()
    {
        _skinnedMeshRenderer.material = _defaultMaterial;
        _scared.SetActive(false);
        _angry.SetActive(true);
    }

    private void ChangeState()
    {

    }
}
