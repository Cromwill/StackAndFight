using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishWall : Interactable
{
    [SerializeField] private bool _enableSlowMotion;
    [SerializeField] private WallLevelUI _wallLevelUI;
    [SerializeField] private CameraImpulseGenerator _cameraImpulseGenerator;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private Currency _currencyPrefab;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _exlodePoint;

    private Brick[] _bricks;
    private List<Currency> _currencies = new List<Currency>();
    private SlowMotion _slowMotion;
    private float _currencyReward;

    public int WallLevel { get; private set; }
    public WallLevelUI WallLevelUI => _wallLevelUI;

    private void Awake()
    {
        _bricks = GetComponentsInChildren<Brick>();
    }

    private void Start()
    {
        _slowMotion = FindObjectOfType<SlowMotion>();
    }

    public void Init(int level)
    {
        WallLevel = level;
        _wallLevelUI.UpdateUI(level);
        _currencyReward = level / 10;
        _currencyReward = Mathf.Clamp(_currencyReward, 1, 1000);
        SpawnCurrency();
    }

    public override void Interact(Player player)
    {
        _cameraImpulseGenerator.ShakeCamera();

        if(WallLevel < player.LevelSystem.Level)
            Break(player);
        else
            player.Die();
    }

    private void Break(Player player)
    {
        _hitEffect.Play();
        player.Wallet.Increase(_currencyReward);

        foreach (var currency in _currencies)
        {
              currency.Rigidbody.isKinematic = false;
              currency.Rigidbody.AddForce(Vector3.forward * 10, ForceMode.VelocityChange);
              currency.Rigidbody.AddTorque(RandomPointInBounds(_boxCollider.bounds).normalized * 180);
        }

        foreach (var brick in _bricks)
        {
            brick.Break();
            brick.Explode(_exlodePoint.position, 15f);
        }

        if (_enableSlowMotion)
            _slowMotion.TriggerSlowMotion();
    }

    private void SpawnCurrency()
    {
        for (int i = 0; i < _currencyReward; i++)
        {
            var currency = Instantiate(_currencyPrefab, transform);
            _currencies.Add(currency);

            currency.transform.position = RandomPointInBounds(new Bounds(transform.position, new Vector3(2f, 1.6f,0.5f)));
        }
    }

    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
