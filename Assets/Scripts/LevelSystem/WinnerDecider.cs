using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerDecider : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    private bool _isWinned;
    private Player _player;

    public Action GameEnded;

    private void Awake()
    {
        if (_winScreen.activeInHierarchy)
            _winScreen.SetActive(false);

        if (_loseScreen.activeInHierarchy)
            _loseScreen.SetActive(false);
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.DeathChecked += EndGame;
    }

    private void OnDisable()
    {
        _player.DeathChecked -= EndGame;
    }

    private void EndGame(Player player)
    {
        _isWinned = true;

        if (player.IsDead)
        {
            SetLose();
        }
        else
        {
            StartCoroutine(DelayedEnable());
            SoundHandler.Instance.PlayWinSound();
        }
    }

    private void SetLose()
    {
        _isWinned = false;
        StartCoroutine(DelayedLose(1.5f));
    }

    private IEnumerator DelayedEnable()
    {
        yield return new WaitForSeconds(1f);

        ShowScreen(_winScreen);
        SaveSystem.DeleteRestarted();
    }

    private IEnumerator DelayedLose(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_isWinned == false)
            ShowScreen(_loseScreen);
    }

    private void ShowScreen(GameObject screen)
    {
        GameEnded?.Invoke();
        screen.SetActive(true);
    }
}
