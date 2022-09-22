using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _differencePrefab;

    private void Start()
    {
        OnLevelChanged(_player.LevelSystem.Level);
    }

    private void OnEnable()
    {
        _player.LevelSystem.LevelChanged += OnLevelChanged;
        _player.LevelSystem.DifferenceSet += OnDifferenceSet;
    } 

    private void OnDisable()
    {
        _player.LevelSystem.LevelChanged -= OnLevelChanged;
        _player.LevelSystem.DifferenceSet -= OnDifferenceSet;
    }

    private void OnLevelChanged(int value)
    {
        _level.text = value.ToString();
    }

    private void OnDifferenceSet(int value)
    {
        _differencePrefab.text = "+ " + value.ToString();

        var difference = Instantiate(_differencePrefab, transform);
        StartCoroutine(Animating(difference));

        //int currentLevel = _player.LevelSystem.Level;
        //int previousLevel = _player.LevelSystem.Level - value;

        //StartCoroutine(ChangingLevel(currentLevel, previousLevel));
    }

    private IEnumerator Animating(TMP_Text difference)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(difference.DOFade(1, 0.2f));
        sequence.Insert(0, difference.transform.DOLocalMoveY(80f, 1.5f));
        sequence.Insert(1, difference.DOFade(0, 0.5f));

        yield return new WaitForSeconds(4f);

        difference.gameObject.SetActive(false);
    }

    //private IEnumerator ChangingLevel(int currentLevel, int previousLevel)
    //{
    //    int level = previousLevel;

    //    while(previousLevel < currentLevel)
    //    {
    //        _level.text = previousLevel.ToString();
    //        previousLevel++;
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //}
}
