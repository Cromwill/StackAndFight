using UnityEngine;

public class EnemyRender : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _scaredMaterial;
    [SerializeField] private Material _deadMaterial;
    [SerializeField] private GameObject _angry;
    [SerializeField] private GameObject _scared;

    public void SetScared()
    {
        _skinnedMeshRenderer.material = _scaredMaterial;
    }

    public void SetDefaultd()
    {
        _skinnedMeshRenderer.material = _defaultMaterial;
    }

    public void SetDead()
    {
        _skinnedMeshRenderer.material = _deadMaterial;
    }

    private void ChangeState()
    {

    }
}
