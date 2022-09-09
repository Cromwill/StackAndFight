using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerCanvasHandler _canvasHandler;
    [SerializeField] private PlayerEffectsHandler _effectsHandler;
    [SerializeField] private LevelSystem _levelSystem;

    private bool _isDead;

    public ValueHandler Wallet { get; private set; } = new ValueHandler(1000, 10000, "WalletSaveWord");
    public ValueHandler MoneyMultiplier { get; private set; } = new ValueHandler(1, 20, "MoneyMultilierSaveWord");
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public LevelSystem LevelSystem => _levelSystem;
    public PlayerMover Mover => _mover;
    public PlayerCanvasHandler CanvasHandler => _canvasHandler;
    public bool IsDead => _isDead;

    public event Action CameraSwitched;
    public event Action<Player> DeathChecked;

    private void Awake()
    {
        _mover.Init(_playerAnimator, _levelSystem);
        _levelSystem.Init();
        Wallet.LoadAmount();
        MoneyMultiplier.LoadAmount();
    }

    private void Start()
    {
        //SoundHandler.Instance.PlayLandingSound();
    }

    public void Die()
    {
        SoundHandler.Instance.PlayLoseSound();
        _isDead = true;
        Fall();
    }

    public void Fall()
    {
        _mover.DisableMovement();
        _mover.PushBack();
        _playerAnimator.TriggerFall();
        _effectsHandler.PlayDeath();
        _canvasHandler.Disable();
        DeathChecked?.Invoke(this);
    }

    public void PushEnemy(Enemy enemy)
    {
        SoundHandler.Instance.PlayPunchSound();
        SoundHandler.Instance.PlayOuchSound();

        float forceValue = 0;
        //forceValue = Mathf.Clamp(forceValue, 80, 120) + LevelSystem.Level;
        forceValue = Mathf.Clamp(forceValue, 80, 120);
        LevelSystem.IncreaseLevel(enemy.Level);
        IncreaseMoney(enemy.Cost);

        Vector3 veloctiy = (transform.forward + transform.up)* forceValue;
        enemy.Push(veloctiy);
    }

    public void IncreaseMoney(float value)
    {
        Wallet.Increase(value * MoneyMultiplier.Value);
    }

    public void KillBoss()
    {
        StartCoroutine(KillingBoss());
    }

    public void OnBuying(UpgradeType upgradeType)
    {
        _playerAnimator.PlayLevelUp();

        if (upgradeType == UpgradeType.StartLevel)
            _effectsHandler.PlayLevelUp();
        else
            _effectsHandler.PlayMoneyUp();
    }

    private void Activate(Collider[] colliders)
    {
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Path path))
            {
                if (path.IsActivated == false)
                    path.Activate();
            }
        }
    }

    private IEnumerator KillingBoss()
    {
        _mover.StopMoving();
        StartCoroutine(AcitvatingAll());

        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;
        DeathChecked?.Invoke(this);
    }

    private IEnumerator AcitvatingAll()
    {
        Collider[] colliders;
        int maxRadius = 5;
        float radius = 1.5f;

        while (radius <= maxRadius)
        {
            colliders = Physics.OverlapSphere(transform.position, radius);
            Activate(colliders);
            radius += 0.5f;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
