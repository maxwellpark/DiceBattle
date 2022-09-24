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
    [SerializeField]
    private float _rowConstraintNum;

    private Player _player;
    private EnemyPlayer _enemyPlayer;
    public static event UnityAction onLowHealth;

    protected override void Start()
    {
        var playerObj = GameObject.FindWithTag("Player");

        if (playerObj == null)
            throw new Exception("Could not find player object.");

        _player = playerObj.GetComponent<Player>();

        var enemyObj = GameObject.FindWithTag("Enemy");

        if (enemyObj == null)
            throw new Exception("Could not find enemy player object.");

        _enemyPlayer = enemyObj.GetComponent<EnemyPlayer>();

        base.Start();
        onShoot += () => StartCoroutine(DelayShooting());
    }

    protected override void Update()
    {
        if (!CanShoot)
            return;

        if (shotsRemaining <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Shoot if player y coord is within accepted range
        if (IsInRowConstraint())
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
        CanShoot = true;
    }

    private IEnumerator DelayShooting()
    {
        CanShoot = false;
        var delay = GetShootDelayInSeconds();
        yield return new WaitForSeconds(delay);
        CanShoot = true;
    }

    protected float GetShootDelayInSeconds()
    {
        if (!_dynamicShooting)
            return _shootDelayInSeconds;

        var delay = UnityEngine.Random.Range(_shootDelayMin, _shootDelayMax);
        return delay;
    }

    protected bool IsInRowConstraint()
    {
        var playerCell = _player.grid.currentCell;
        var enemyCell = _enemyPlayer.grid.currentCell;

        if (playerCell == null || enemyCell == null)
            return false;

        var difference = Mathf.Abs(playerCell.yCoord - enemyCell.yCoord);
        return difference <= _rowConstraintNum;
    }

    protected override void RegisterEvents()
    {

    }
}
