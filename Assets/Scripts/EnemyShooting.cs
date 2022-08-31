using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShooting : PlayerShooting
{
    [SerializeField]
    private float _shootDelayInSeconds;
    [SerializeField]
    private bool _dynamicShooting;
    [SerializeField]
    private float _shootDelayMin;
    [SerializeField]
    private float _shootDelayMax;

    private bool _canShoot = true;
    private Player _player;
    private EnemyPlayer _enemyPlayer;
    public static event UnityAction onLowHealth;

    protected override void Start()
    {
        var _playerObj = GameObject.FindWithTag("Player");

        if (_playerObj == null)
            throw new Exception("Could not find player object.");

        _player = _playerObj.GetComponent<Player>();

        var _enemyObj = GameObject.FindWithTag("Enemy");

        if (_enemyObj == null)
            throw new Exception("Could not find enemy player object.");

        _enemyPlayer = _enemyObj.GetComponent<EnemyPlayer>();

        base.Start();
        onShoot += () => StartCoroutine(DelayShooting());
    }

    protected override void Update()
    {
        if (!_canShoot)
            return;

        if (shotsRemaining <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        var playerCell = _player.grid.currentCell;
        var enemyCell = _enemyPlayer.grid.currentCell;

        //if (playerCell != null && enemyCell != null && playerCell.yCoord == enemyCell.yCoord)
        //    Shoot();

        Shoot();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        onLowHealth?.Invoke();
    }

    public override void SetupShooting(int magSize)
    {
        base.SetupShooting(magSize);
        _canShoot = true;
    }

    private IEnumerator DelayShooting()
    {
        _canShoot = false;
        var delay = GetShootDelayInSeconds();
        yield return new WaitForSeconds(delay);
        _canShoot = true;
    }

    protected float GetShootDelayInSeconds()
    {
        if (!_dynamicShooting)
            return _shootDelayInSeconds;

        var delay = UnityEngine.Random.Range(_shootDelayMin, _shootDelayMax);
        return delay;
    }

    protected override void RegisterEvents()
    {

    }
}
