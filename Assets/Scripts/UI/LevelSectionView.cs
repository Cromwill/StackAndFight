using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSectionView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Color _activeBackgroundColor;
    [SerializeField] private Color _inactiveBackgroundColor;

    public void ActivateBackground()
    {
        _background.color = _activeBackgroundColor;
    }

    public void DeactivateBackground()
    {
        _background.color = _inactiveBackgroundColor;
    }

    public void Fill(float value, float maxValue)
    {
        _fillImage.fillAmount = value / maxValue;
    }

}
