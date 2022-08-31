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
    }

    public override void Interact(Player player)
    {
        _cameraImpulseGenerator.ShakeCamera();

        if(WallLevel < player.LevelSystem.Level)
            Break(player);
        else
            player.Fall();
    }

    private void Break(Player player)
    {
        _hitEffect.Play();
        player.Wallet.Increase(_currencyReward);
        SpawnCurrency();

        foreach (var brick in _bricks)
        {
            brick.Break();
            brick.Explode(player.transform.position, 10f);
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

            currency.transform.position = RandomPointInBounds(_boxCollider.bounds);
            currency.transform.rotation = Random.rotation;

            if (currency.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.isKinematic = false;
                rigidbody.AddForce(Vector3.forward*10, ForceMode.VelocityChange);
                rigidbody.AddTorque(RandomPointInBounds(_boxCollider.bounds).normalized * 180);
            }
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
