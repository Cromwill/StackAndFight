using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelSectionHandler : MonoBehaviour
{
    private LevelSectionTrigger[] _levelSectionTriggers;

    private List<LevelSectionView> _levelSectionViews = new List<LevelSectionView>();

    public LevelSectionView CurrentLevelSectionView { get; private set; }

    private void Start()
    {
        _levelSectionTriggers = FindObjectsOfType<LevelSectionTrigger>();

        _levelSectionViews = GetComponentsInChildren<LevelSectionView>().ToList();

        foreach (var trigger in _levelSectionTriggers)
        {
            _levelSectionViews.Add(trigger.LevelSectionView);
        }

        foreach (var section in _levelSectionViews)
        {
            DeactivateSectionView(section);
        }

        SetCurrentSectionView(_levelSectionViews[0]);
    }

    public void ActivateSectionView(LevelSectionView currentSectionView)
    {
        foreach (var sectionView in _levelSectionViews)
        {
            if (sectionView != currentSectionView)
                DeactivateSectionView(sectionView);
        }

        SetCurrentSectionView(currentSectionView);
    }

    private void SetCurrentSectionView(LevelSectionView sectionView)
    {
        sectionView.ActivateBackground();
        CurrentLevelSectionView = sectionView;
    }

    private void DeactivateSectionView(LevelSectionView sectionView)
    {
        sectionView.DeactivateBackground();
    }
}
