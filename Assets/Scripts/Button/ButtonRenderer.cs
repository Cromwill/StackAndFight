using UnityEngine;

public class ButtonRenderer : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _notPressedMaterial;
    [SerializeField] private Material _pressedMaterial;

    public void ChangeToPressed()
    {
        _renderer.material = _pressedMaterial;
    }

    public void ChangeToUnpressed()
    {
        _renderer.material = _notPressedMaterial;
    }
}
