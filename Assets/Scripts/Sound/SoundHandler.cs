using System.Collections;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _lose;
    [SerializeField] private AudioSource _landing;
    [SerializeField] private AudioSource _punch;
    [SerializeField] private AudioSource _kick;
    [SerializeField] private AudioSource _scream;
    [SerializeField] private AudioSource _ouch;
    [SerializeField] private AudioSource _gem;
    [SerializeField] private AudioSource _smash;
    [SerializeField] private AudioSource _button;
    [SerializeField] private AudioSource _arrow;
    [SerializeField] private AudioSource _backgroundBlue;
    [SerializeField] private AudioSource _backgroundOrange;
    [SerializeField] private AudioSource _backgroundPurple;
    [SerializeField] private AudioSource _backgroundGreen;

    private LevelSetting _currentLevelSetting;
    private AudioSource _currentBackground;

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
        _backgroundBlue.Play();
        _currentBackground = _backgroundBlue;
        var settingDecider = FindObjectOfType<LevelSettingDecider>();
        _currentLevelSetting = settingDecider.LevelSetting;

        if(_currentLevelSetting != LevelSetting.Blue)
        {
            _backgroundBlue.Stop();
        }
    }

    public void PlayWinSound()
    {
        _win.Play();
    }

    public void StopWinSound()
    {
        if(_win.isPlaying)
            _win.Stop();
    }

    public void PlayLoseSound()
    {
        _kick.Play();
        _scream.Play();
        StartCoroutine(StartingLoseSound(_currentBackground));
    }

    public void PlayScreamSound()
    {
        _scream.Play();
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

        ////RandomizePitch(_gem);
        //_gem.Play();
    }

    public void PlaySmashSound()
    {
        RandomizePitch(_smash);
        _smash.Play();
    }

    public void PlayButtonSound()
    {
        _button.Play();
    }

    public void PlayArrowSound()
    {
        _arrow.Play();
    }

    public void PlayBackground(LevelSetting levelSetting)
    {
        if (_currentLevelSetting == levelSetting)
            return;

        if(_currentBackground != null)
            _currentBackground.Stop();

        if (levelSetting == LevelSetting.Blue)
        {
            _backgroundBlue.Play();
            _currentBackground = _backgroundBlue;
        }

        if (levelSetting == LevelSetting.Orange)
        {
            _backgroundOrange.Play();
            _currentBackground = _backgroundGreen;
        }

        if (levelSetting == LevelSetting.Purple)
        {
            _backgroundPurple.Play();
            _currentBackground = _backgroundPurple;
        }

        if (levelSetting == LevelSetting.Green)
        {
            _backgroundGreen.Play();
            _currentBackground = _backgroundGreen;
        }

        _currentLevelSetting = levelSetting;
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
