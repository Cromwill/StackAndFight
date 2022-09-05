using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private RagdollHandler _ragdollHandler;
    [SerializeField] private Collider _collider;
    [SerializeField] private EnemyLevelCounter _levelCounter;
    [SerializeField] private Shield _shield;
    [SerializeField] private EnemyRotation _rotation;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyRender _enemyRender;
    [SerializeField] private EnemyEffectsHandler _enemyEffectsHandler;
    [SerializeField] private CameraImpulseGenerator _cameraImpulseGenerator;

    private Player _player;
    private bool _isDead;
    private string _id;
    private int _savedLoop;
    private string _loopSaveWord;
    private int _currentLevel;

    public int Cost { get; private set; } = 5;
    public EnemyAnimator EnemyAnimator => _animator;
    public RagdollHandler RagdollHandler => _ragdollHandler;
    public EnemyRotation Rotation => _rotation;
    public int InititalLevel => _level;
    public int Level { get; private set; }

    public event Action<Enemy> Died;

    private void Update()
    {
        if (_isDead)
            return;

        if (_player.LevelSystem.Level < Level)
        {
            //_enemyRender.SetDefaultd();
            _animator.SetAggresive();
        }
        else
        {
            _animator.SetDefault();
            //_enemyRender.SetScared();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if (player.LevelSystem.Level >= Level)
                player.PushEnemy(this);
        }
    }

    public void Init(Player player,int level, int additonalLevels, int index)
    {
        _id = $"{index}{SceneManager.GetActiveScene().name}";
        _loopSaveWord = $"{_id}loop";
        _player = player;
        _rotation.Init(_player);
        _currentLevel = level;

        if (this is Boss)
            _level = 0;

        Level = LoadLevel(additonalLevels);
    }

    public void Push(Vector3 direction)
    {
        _isDead = true;

        _cameraImpulseGenerator.ShakeCamera();
        _enemyRender.SetDead();
        _enemyEffectsHandler.PlayDeathEffect();

        if (_shield != null)
        {
            _shield.Drop();
        }

        _collider.enabled = false;
        _levelCounter.gameObject.SetActive(false);
        RagdollHandler.ActivateRagdoll();
        RagdollHandler.Chest.Push(direction);
        _rotation.Disable();
        Died?.Invoke(this);
        StartCoroutine(GoingDown());
    }

    public void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);

    }

    private IEnumerator GoingDown()
    {
        float elapsedTime = 0;

        yield return new WaitForSeconds(5f);

        GetComponent<Collider>().enabled = false;
        _ragdollHandler.DeactivateRagdoll();

        while (elapsedTime < 5)
        {
            transform.position += Vector3.down * Time.deltaTime * 5f;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private int LoadLevel(float additonalLevels)
    {
        _savedLoop = SaveSystem.LoadLevelLoop();

        if (PlayerPrefs.HasKey(_id) && PlayerPrefs.HasKey(_loopSaveWord) && _savedLoop == PlayerPrefs.GetInt(_loopSaveWord))
            return PlayerPrefs.GetInt(_id);

        int level = (int)(_currentLevel + (int)_player.LevelSystem.AdditionalLevel.Value - 2 + (int)additonalLevels);
        level = Mathf.Clamp(level, 1, 1000);

        PlayerPrefs.SetInt(_id, level);
        PlayerPrefs.SetInt(_loopSaveWord, _savedLoop);

        return level;
    }
}
