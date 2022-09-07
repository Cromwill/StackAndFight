using System.Collections;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _lose;
    [SerializeField] private AudioSource _landing;
    [SerializeField] private AudioSource _punch;
    [SerializeField] private AudioSource _ouch;
    [SerializeField] private AudioSource _gem;
    [SerializeField] private AudioSource _smash;
    [SerializeField] private AudioSource _backgroundBlue;
    [SerializeField] private AudioSource _backgroundOrange;
    [SerializeField] private AudioSource _backgroundPurple;
    [SerializeField] private AudioSource _backgroundGreen;

    public LevelSetting _currentLevelSetting;

    public static SoundHandler Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

       Destroy(gameObject);
    }

    private void Start()
    {
        //_backgroundBlue.Play();
        var settingDecider = FindObjectOfType<LevelSettingDecider>();
        _currentLevelSetting = settingDecider.LevelSetting;
    }

    public void PlayWinSound()
    {
        _win.Play();
        //IncreaseLevelCounter();
    }

    public void StopWinSound()
    {
        _win.Stop();
    }

    public void PlayLoseSound()
    {
        //StartCoroutine(StartingLoseSound(_currentBackground));
    }

    public void PlayOuchSound()
    {
        RandomizePitch(_ouch);
        _ouch.Play();
    }

    public void PlayLandingSound()
    {
        _landing.Play();
    }

    public void PlayPunchSound()
    {
        RandomizePitch(_punch);
        _punch.Play();
    }

    public void PlayGemSound()
    {
        //if (_gem.isPlaying)
        //    return;

        //RandomizePitch(_gem);
        _gem.Play();
    }

    public void PlaySmashSound()
    {
        RandomizePitch(_smash);
        _smash.Play();
    }

    public void PlayBackground(LevelSetting levelSetting)
    {
        //if (_currentLevelSetting == levelSetting)
        //    return;

        //if (levelSetting == LevelSetting.Blue)
        //    _backgroundBlue.Play();

        //if (levelSetting == LevelSetting.Orange)
        //    _backgroundOrange.Play();

        //if (levelSetting == LevelSetting.Purple)
        //    _backgroundPurple.Play();

        //if (levelSetting == LevelSetting.Green)
        //    _backgroundGreen.Play();

        //_currentLevelSetting = levelSetting;
    }

    private void RandomizePitch(AudioSource sound)
    {
        sound.pitch = Random.Range(0.8f, 1.2f);
    }

    private IEnumerator StartingLoseSound(AudioSource background)
    {
        background.Pause();
        _lose.Play();

        yield return new WaitUntil(() => _lose.isPlaying == false);

        background.UnPause();
    }
}
