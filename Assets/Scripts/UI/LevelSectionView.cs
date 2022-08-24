using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSectionView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _fillImage;
    [SerializeField] private TMP_Text _number;
    [SerializeField] private ButtonAnimation _animation;
    [SerializeField] private Color _activeBackgroundColor;
    [SerializeField] private Color _inactiveBackgroundColor;

    public void Init(int sectionIndex)
    {
        _number.text = $"{sectionIndex}";
    }

    public void ActivateBackground()
    {
        _background.color = _activeBackgroundColor;
        _animation.PlayAnimation();
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
