using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelSectionTrigger : MonoBehaviour
{
    [SerializeField] private LevelSectionView _levelSectionView;
    [SerializeField] private LevelSectionHandler _levelSectionHandler;

    public LevelSectionView LevelSectionView => _levelSectionView;

    private void OnTriggerEnter(Collider other)
    {
        _levelSectionHandler.ActivateSectionView(_levelSectionView);
    }
}
